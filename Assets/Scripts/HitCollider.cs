using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnValidate()
    {
        if (_health == null)
            _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Damager damager))
        {
            _health.TakeDamage(damager.Damage);
        }
        else if (collision.gameObject.TryGetComponent(out Player player))
        {
            _health.Die();
        }
    }
}
