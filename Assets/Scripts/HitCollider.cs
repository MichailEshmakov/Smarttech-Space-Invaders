using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField] private Health _health;

    private List<Type> _safeTypes = new List<Type>(); 

    private void OnValidate()
    {
        if (_health == null)
            _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Damager damager))
        {
            bool isSafeType = false;
            foreach (Type type in _safeTypes)
            {
                if (damager.GetComponent(type) != null)
                {
                    isSafeType = true;
                    break;
                }
            }

            if (isSafeType == false)
                _health.TakeDamage(damager.Damage);
        }
        else if (collision.gameObject.TryGetComponent(out Player player))
        {
            _health.Die();
        }
    }

    public void AddSafeType(Type type)
    {
        if (_safeTypes.Contains(type) == false)
            _safeTypes.Add(type);
    }
}
