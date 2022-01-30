using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    
    private int _value;

    public int Value => _value;

    public event UnityAction ValueChanged;

    private void OnEnable()
    {
        _enemySpawner.EnemyDead += OnEnemyDead;
    }

    private void OnDisable()
    {
        _enemySpawner.EnemyDead -= OnEnemyDead;
    }

    private void OnEnemyDead(Enemy deadEnemy)
    {
        _value += deadEnemy.Score;
        ValueChanged?.Invoke();
    }
}
