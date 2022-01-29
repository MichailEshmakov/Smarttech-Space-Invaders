using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] [Min(1)] private int _value;

    private bool _isDead;

    public int Value => _value;

    public event UnityAction Dead;
    public event UnityAction ValueChanged;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            Debug.LogError(nameof(damage) + " is less, than zero");

        _value = (int)Mathf.MoveTowards(_value, 0, damage);
        ValueChanged?.Invoke();

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
