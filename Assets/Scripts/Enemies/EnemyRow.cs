using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRow : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private float _horisontalSpeed;
    [SerializeField] private float _resultHorisintalSpeed;

    private List<EnemySubRow> _subRows = new List<EnemySubRow>();
    private float _speedStepPerDeath;

    public event UnityAction<EnemyRow> Destroyed;
    public event UnityAction<Enemy> EnemyDead;

    private void Awake()
    {
        CreateSubRow(_enemies, Vector2.left);
        _speedStepPerDeath = (_resultHorisintalSpeed - _horisontalSpeed) / (_enemies.Count - 1);

        foreach (Enemy enemy in _enemies)
        {
            enemy.Dead += OnEnemyDead;
        }
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Dead -= OnEnemyDead;
        }

        foreach (EnemySubRow subRow in _subRows)
        {
            UnsubscribeOnSubRow(subRow);
        }

        Destroyed?.Invoke(this);
    }

    public void SetFirstness()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.SetFirstRow();
        }
    }

    private void OnEnemyDead(Enemy deadEnemy)
    {
        _enemies.Remove(deadEnemy);
        deadEnemy.Dead -= OnEnemyDead;

        if (_enemies.Count == 0)
            Destroy(gameObject);

        _horisontalSpeed += _speedStepPerDeath;

        foreach (EnemySubRow subRow in _subRows)
        {
            subRow.SetHorisontalSpeed(_horisontalSpeed);
        }

        EnemyDead?.Invoke(deadEnemy);
    }

    private void OnSubRowDivided(IReadOnlyList<Enemy> leftEnemies, Vector2 movingDirection)
    {
        CreateSubRow(leftEnemies, -movingDirection);
    }

    private void CreateSubRow(IReadOnlyList<Enemy> enemies, Vector2 movingDirection)
    {
        EnemySubRow newSubRow = new GameObject(nameof(EnemySubRow)).AddComponent<EnemySubRow>();
        newSubRow.transform.parent = transform;
        newSubRow.transform.position = transform.position;
        newSubRow.Init(_horisontalSpeed, enemies, Vector2.left);
        SubscribeOnSubRow(newSubRow);
        _subRows.Add(newSubRow);
    }

    private void OnSubRowDestroyed(EnemySubRow destroyedSubRow)
    {
        _subRows.Remove(destroyedSubRow);
        UnsubscribeOnSubRow(destroyedSubRow);

        if (_subRows.Count == 0)
            Destroy(gameObject);
    }

    private void SubscribeOnSubRow(EnemySubRow subRow)
    {
        subRow.Divided += OnSubRowDivided;
        subRow.Destroyed += OnSubRowDestroyed;
    }

    private void UnsubscribeOnSubRow(EnemySubRow subRow)
    {
        subRow.Divided -= OnSubRowDivided;
        subRow.Destroyed -= OnSubRowDestroyed;
    }
}
