using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;

    private void OnValidate()
    {
        if (_enemySpawner == null)
            _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        _enemySpawner.StartNextWave();
    }
}
