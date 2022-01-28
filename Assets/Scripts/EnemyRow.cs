using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Gabarits))]
public class EnemyRow : MonoBehaviour
{
    private Mover _mover;
    private Gabarits _gabarits;

    private List<Enemy> _enemies;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        
        TrySetEnemies();
        InitGabarits();
    }

    private void TrySetEnemies()
    {
        _enemies = GetComponentsInChildren<Enemy>().ToList();
        if (_enemies.Count == 0)
        {
            Debug.LogError($"There are not enemies in row {gameObject.name}");
            Destroy(gameObject);
        }
    }

    private void InitGabarits()
    {
        _gabarits = GetComponent<Gabarits>();
        float leftestExtremePoint = _enemies.Select(enemy => enemy.LeftExtremeCoordinate).Min();
        float rightestExtremePoint = _enemies.Select(enemy => enemy.RightExtremeCoordinate).Max();
        float toppestExtremePoint = _enemies.Select(enemy => enemy.TopExtremeCoordinate).Max();
        float bottomestExtremePoint = _enemies.Select(enemy => enemy.BottomExtremeCoordinate).Min();
        _gabarits.Init(leftestExtremePoint, rightestExtremePoint, toppestExtremePoint, bottomestExtremePoint);
    }
}
