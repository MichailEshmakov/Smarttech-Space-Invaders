using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDestroyer : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnValidate()
    {
        if (_health == null)
            _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Dead += OnDead;
    }

    private void OnDisable()
    {
        _health.Dead -= OnDead;
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }
}
