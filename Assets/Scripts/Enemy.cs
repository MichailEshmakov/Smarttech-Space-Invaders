using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Health _health;

    public event UnityAction<Enemy> Dead;
    public event UnityAction BorderCollided;
    public event UnityAction EnemyCollided;

    private void OnValidate()
    {
        if (_health == null)
            _health = GetComponent<Health>();

        if (_weapon == null)
            _weapon = GetComponent<Weapon>();
    }

    private void OnEnable()
    {
        _weapon.ToFirePrepared += OnWeaponPrepared;
        _health.Dead += OnDead;
    }

    private void OnDisable()
    {
        _weapon.ToFirePrepared -= OnWeaponPrepared;
        _health.Dead -= OnDead;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out SideBorder sideBorder))
        {
            BorderCollided?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            EnemyCollided?.Invoke();
        }
    }

    private void OnDead()
    {
        Dead?.Invoke(this);
    }

    private void OnWeaponPrepared()
    {
        _weapon.TryFire();
    }
}