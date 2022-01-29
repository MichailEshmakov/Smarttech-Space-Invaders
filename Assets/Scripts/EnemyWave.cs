using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWave : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private List<EnemyRow> _rows;

    public event UnityAction Destroyed;

    private void OnValidate()
    {
        if (_mover == null)
            _mover = GetComponent<Mover>();
    }

    private void Awake()
    {
        foreach (EnemyRow row in _rows)
        {
            row.Destroyed += OnRowDestroyed;
        }

        _rows[0].SetFirstness();
    }

    private void Update()
    {
        _mover.Move(Vector2.down);
    }

    private void OnDestroy()
    {
        foreach (EnemyRow row in _rows)
        {
            row.Destroyed -= OnRowDestroyed;
        }

        Destroyed?.Invoke();
    }

    private void OnRowDestroyed(EnemyRow row)
    {
        row.Destroyed -= OnRowDestroyed;

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
