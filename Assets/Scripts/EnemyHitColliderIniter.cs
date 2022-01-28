using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitColliderIniter : MonoBehaviour
{
    [SerializeField] private HitCollider _hitCollider;

    private void OnValidate()
    {
        if (_hitCollider == null)
            _hitCollider = GetComponent<HitCollider>();
    }

    private void Awake()
    {
        _hitCollider.AddSafeType(typeof(Enemy));
    }
}
