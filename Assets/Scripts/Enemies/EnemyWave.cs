using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWave : MonoBehaviour
{
    private Mover _mover;
    private List<EnemyRow> _rows = new List<EnemyRow>();

    public event UnityAction<EnemyWave> Destroyed;
    public event UnityAction<Enemy> EnemyDead;

    private void Update()
    {
        _mover.Move(Vector2.down);
    }

    private void OnDestroy()
    {
        foreach (EnemyRow row in _rows)
        {
            row.Destroyed -= OnRowDestroyed;
            row.EnemyDead -= OnEnemyDead;
        }

        Destroyed?.Invoke(this);
    }

    public void Init(EnemyWaveData waveData, float positonLimit)
    {
        InitMover(waveData.Speed);
        InitRows(waveData.Rows, waveData.DistanceBetweenRows, positonLimit);
        _rows[0].SetFirstness();
    }

    private void InitMover(float speed)
    {
        if (_mover == null)
            _mover = gameObject.AddComponent<Mover>();

        _mover.SetSpeed(speed);
    }

    private void InitRows(IReadOnlyList<EnemyRowData> rowDatas, float distanceBetweenRows, float positonLimit)
    {
        for (int i = 0; i < rowDatas.Count; i++)
        {
            EnemyRow newRow = new GameObject(nameof(EnemyRow)).AddComponent<EnemyRow>();
            newRow.transform.position = transform.position + Vector3.up * i * distanceBetweenRows;
            newRow.transform.SetParent(transform);
            newRow.Init(rowDatas[i]);
            _rows.Add(newRow);
            CoordinateMovementLimiter limiter = newRow.gameObject.AddComponent<CoordinateMovementLimiter>();
            limiter.Init(CoordinateLitera.y, false, positonLimit);// TODO: Реализовать ограничение с учетом габаритов и нескольких рядов

            newRow.Destroyed += OnRowDestroyed;
            newRow.EnemyDead += OnEnemyDead;
        }
    }

    private void OnEnemyDead(Enemy deadEnemy)
    {
        EnemyDead?.Invoke(deadEnemy);
    }

    private void OnRowDestroyed(EnemyRow row)
    {
        row.Destroyed -= OnRowDestroyed;
        row.EnemyDead -= OnEnemyDead;

        if (_rows.IndexOf(row) == 0)
        {
            if (_rows.Count == 1)
                Destroy(gameObject);
            else
                _rows[1].SetFirstness();
        }

        _rows.Remove(row);
    }
}
