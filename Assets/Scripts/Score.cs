using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private EnemyWave _enemyWave;
    
    private int _value;

    public int Value => _value;

    public event UnityAction ValueChanged;

    private void OnEnable()
    {
        _enemyWave.EnemyDead += OnEnemyDead;
    }

    private void OnDisable()
    {
        _enemyWave.EnemyDead -= OnEnemyDead;
    }

    private void OnEnemyDead(Enemy deadEnemy)
    {
        _value += deadEnemy.Score;
        ValueChanged?.Invoke();
    }
}
