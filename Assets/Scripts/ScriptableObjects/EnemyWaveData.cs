using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Wave")]
public class EnemyWaveData : ScriptableObject
{
    [SerializeField] private List<EnemyRowData> _rows;
    [SerializeField] private float _distanceBetweenRows;
    [SerializeField] private float _maxWidth;
    [SerializeField] private float _speed;

    public IReadOnlyList<EnemyRowData> Rows => _rows;
    public float DistanceBetweenRows => _distanceBetweenRows;
    public float Speed => _speed;

    private void OnValidate()
    {
        foreach (EnemyRowData row in _rows)
        {
            if (row.ComputeWidth() > _maxWidth)
                row.Shrink(_maxWidth);
        }
    }
}
