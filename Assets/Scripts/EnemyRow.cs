using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRow : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private float _speed;
    [SerializeField] private float _resultSpeed;

    private List<EnemySubRow> _subRows = new List<EnemySubRow>();
    private float _speedStepPerDeath;

    private void Awake()
    {
        CreateSubRow(_enemies, Vector2.left);
        _speedStepPerDeath = (_resultSpeed - _speed) / (_enemies.Count - 1);

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
    }

    private void OnEnemyDead(Enemy deadEnemy)
    {
        _enemies.Remove(deadEnemy);
        deadEnemy.Dead -= OnEnemyDead;

        if (_enemies.Count == 0)
            Destroy(gameObject);

        _speed += _speedStepPerDeath;

        foreach (EnemySubRow subRow in _subRows)
        {
            subRow.SetSpeed(_speed);
        }
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
        newSubRow.Init(_speed, enemies, Vector2.left);
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
