using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class EnemyRowData
{
    [SerializeField] private float _horisontalSpeed;
    [SerializeField] private float _resultHorisontalSpeed;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private float _betweenEnemiesDistance;

    public float HorisontalSpeed => _horisontalSpeed;
    public float ResultHorisontalSpeed => _resultHorisontalSpeed;
    public IReadOnlyList<Enemy> Enemies => _enemies;
    public float BetweenEnemiesDistance => _betweenEnemiesDistance;

    public float ComputeWidth()
    {
        float width = (_enemies.Count - 1) * _betweenEnemiesDistance;
        foreach (Enemy enemy in _enemies)
        {
            width += enemy.LeftWidth + enemy.RightWidth;
        }

        return width;
    }

    public void Shrink(float maxWidth)
    {
        if (maxWidth >= 0)
        {
            while (ComputeWidth() > maxWidth)
            {
                _enemies.RemoveAt(_enemies.Count - 1);
            }
        }
        else
        {
            Debug.LogError(nameof(maxWidth) + " should not be less zero");
        }
    }
}
