using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRow : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private float _speed;

    private List<EnemySubRow> _subRows = new List<EnemySubRow>();

    private void Awake()
    {
        EnemySubRow newSubRow = new GameObject(nameof(EnemySubRow)).AddComponent<EnemySubRow>();
        newSubRow.transform.parent = this.transform;
        newSubRow.transform.position = transform.position;
        newSubRow.Init(_speed, _enemies, Vector2.left);
    }
}
