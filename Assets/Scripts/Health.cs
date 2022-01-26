using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] [Min(1)] private int _value;

    private bool _isDead;

    public event UnityAction Dead;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            Debug.LogError(nameof(damage) + " is less, than zero");
        _value -= damage;

        if (_isDead == false && _value <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _isDead = true;
        Dead?.Invoke();
    }
}
