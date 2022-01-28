using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Gabarits))]
public class EnemySubRow : MonoBehaviour
{
    private Mover _mover;
    private Gabarits _gabarits;
    private List<Enemy> _enemies;
    private Vector2 _movingDirection;
    private Enemy _rightEnemy;
    private Enemy _leftEnemy;

    public event UnityAction<IReadOnlyList<Enemy>, Vector2> Divided;
    public event UnityAction<EnemySubRow> Destroyed;

    private void OnEnable()
    {
        SubscribeOnEnemies(_enemies);
    }

    private void OnDisable()
    {
        UnsubscribeOnEnemies(_enemies);
    }

    private void Update()
    {
        _mover.Move(_movingDirection);
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
    }

    public void Init(float speed, IReadOnlyList<Enemy> enemies, Vector2 movingDirection)
    {
        _mover = GetComponent<Mover>();
        _mover.SetSpeed(speed);
        _enemies = new List<Enemy>(enemies);
        _movingDirection = movingDirection;
        InitGabarits();
        SubscribeOnEnemies(enemies);

        foreach (Enemy enemy in _enemies)
        {
            enemy.transform.SetParent(transform);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        _mover.SetSpeed(newSpeed);
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
        else
            InitGabarits();
    }

    private void OnMiddleEnemyDead(Enemy deadEnemy)
    {
        IReadOnlyList<Enemy> leftEnemies = _enemies.GetRange(0, _enemies.IndexOf(deadEnemy));

        Divided?.Invoke(leftEnemies, _movingDirection);
        
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
        ChangeDirection();
    }

    private void OnEnemyBorderReached()
    {
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        _movingDirection *= -1;
    }

    private void InitGabarits()
    {
        _gabarits = GetComponent<Gabarits>();
        _enemies = _enemies.OrderByDescending(enemy => enemy.transform.position.x).ToList();
        _leftEnemy = _enemies.LastOrDefault();
        _rightEnemy = _enemies.FirstOrDefault();

        float leftestExtremePoint = _leftEnemy.LeftExtremeCoordinate;
        float rightestExtremePoint = _rightEnemy.RightExtremeCoordinate;

        float toppestExtremePoint = _enemies.Select(enemy => enemy.TopExtremeCoordinate).Max();
        float bottomestExtremePoint = _enemies.Select(enemy => enemy.BottomExtremeCoordinate).Min();
        _gabarits.Init(leftestExtremePoint, rightestExtremePoint, toppestExtremePoint, bottomestExtremePoint);
    }
}
