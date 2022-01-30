using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemySubRowMover))]
public class EnemySubRow : MonoBehaviour
{
    private EnemySubRowMover _mover;
    private List<Enemy> _enemies = new List<Enemy>();
    private Enemy _rightEnemy;
    private Enemy _leftEnemy;

    public event UnityAction<IReadOnlyList<Enemy>, Vector2> Divided;
    public event UnityAction<EnemySubRow> Destroyed;

    private void OnDestroy()
    {
        UnsubscribeOnEnemies(_enemies);
        Destroyed?.Invoke(this);
    }

    public void Init(float speed, IReadOnlyList<Enemy> enemies, Vector2 movingDirection)
    {
        _mover = GetComponent<EnemySubRowMover>();
        _mover.Init(speed, movingDirection);
        _enemies = enemies.ToList();

        SubscribeOnEnemies(enemies);

        foreach (Enemy enemy in _enemies)
        {
            enemy.transform.SetParent(transform);
        }
    }

    public void SetHorisontalSpeed(float value)
    {
        _mover.SetSpeed(value);
    }

    private void OnEnemyDead(Enemy deadEnemy)
    {
        if (_enemies.Contains(deadEnemy))
        {
            if (deadEnemy == _rightEnemy || deadEnemy == _leftEnemy)
                OnExtremeEnemyDead(deadEnemy);
            else
                OnMiddleEnemyDead(deadEnemy);
        }
        else
        {
            Debug.LogError($"{gameObject.name} {nameof(EnemySubRow)} was subscribed on wrong {nameof(Enemy)}");
        }
    }

    private void OnExtremeEnemyDead(Enemy deadEnemy)
    {
        _enemies.Remove(deadEnemy);
        UnsubscribeOnEnemy(deadEnemy);
        if (_enemies.Count == 0)
            Destroy(gameObject);
    }

    private void OnMiddleEnemyDead(Enemy deadEnemy)
    {
        IReadOnlyList<Enemy> leftEnemies = _enemies.GetRange(0, _enemies.IndexOf(deadEnemy));

        Divided?.Invoke(leftEnemies, _mover.MovingDirection);
        
        UnsubscribeOnEnemies(leftEnemies);
        UnsubscribeOnEnemy(deadEnemy);
        _enemies.RemoveRange(0, _enemies.IndexOf(deadEnemy) + 1);
    }

    private void SubscribeOnEnemy(Enemy enemy)
    {
        enemy.BorderCollided += OnEnemyBorderReached;
        enemy.EnemyCollided += OnEnemiesCollided;
        enemy.Dead += OnEnemyDead;
    }

    private void UnsubscribeOnEnemy(Enemy enemy)
    {
        enemy.BorderCollided -= OnEnemyBorderReached;
        enemy.EnemyCollided -= OnEnemiesCollided;
        enemy.Dead -= OnEnemyDead;
    }

    private void UnsubscribeOnEnemies(IReadOnlyList<Enemy> enemies)
    {
        if (enemies != null)
        {
            foreach (Enemy enemy in enemies)
            {
                UnsubscribeOnEnemy(enemy);
            }
        }
    }

    private void SubscribeOnEnemies(IReadOnlyList<Enemy> enemies)
    {
        if (enemies != null)
        {
            foreach (Enemy enemy in enemies)
            {
                SubscribeOnEnemy(enemy);
            }
        }
    }

    private void OnEnemiesCollided()
    {
        _mover.ChangeDirection();
    }

    private void OnEnemyBorderReached()
    {
        _mover.ChangeDirection();
    }
}
