using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyWaveData> _waves;

    private int _currentWaveIndex = -1;

    public event UnityAction<Enemy> EnemyDead;
    public event UnityAction WaveDestroyed;

    public void StartNextWave()
    {
        _currentWaveIndex++;

        if (_currentWaveIndex >= _waves.Count)
            _currentWaveIndex = 0;

        InstantiateWave(_waves[_currentWaveIndex]);
    }

    private void InstantiateWave(EnemyWaveData waveData)
    {
        EnemyWave newWawe = new GameObject(nameof(EnemyWave)).AddComponent<EnemyWave>();
        newWawe.transform.position = transform.position;
        newWawe.Init(waveData);
        newWawe.Destroyed += OnWaweDestroyed;
        newWawe.EnemyDead += OnEnemyDead;
    }

    private void OnEnemyDead(Enemy deadEnemy)
    {
        EnemyDead?.Invoke(deadEnemy);
    }

    private void OnWaweDestroyed(EnemyWave destroyedWave)
    {
        destroyedWave.Destroyed -= OnWaweDestroyed;
        destroyedWave.EnemyDead -= OnEnemyDead;
        WaveDestroyed?.Invoke();
    }
}
