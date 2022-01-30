using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanelShower : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void OnValidate()
    {
        if (_enemySpawner == null)
            _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnEnable()
    {
        _enemySpawner.WaveDestroyed += OnWaveDestroyed;
    }

    private void OnDisable()
    {
        _enemySpawner.WaveDestroyed -= OnWaveDestroyed;
    }

    private void OnWaveDestroyed()
    {
        _winPanel.SetActive(true);
    }
}
