using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Gabarits))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Health _health;

    private Mover _mover;
    private Gabarits _gabarits;

    public event UnityAction Dead;

    public float LeftExtremeCoordinate => _gabarits.LeftExtremeCoordinate;
    public float RightExtremeCoordinate => _gabarits.RightExtremeCoordinate;
    public float TopExtremeCoordinate => _gabarits.TopExtremeCoordinate;
    public float BottomExtremeCoordinate => _gabarits.BottomExtremeCoordinate;

    private void OnValidate()
    {
        if (_health == null)
            _health = GetComponent<Health>();

        if (_weapon == null)
            _weapon = GetComponent<Weapon>();

        _mover = GetComponent<Mover>();
        _gabarits = GetComponent<Gabarits>();
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

    private void Update()
    {
        _mover.Move(Vector2.down);
    }

    private void OnDead()
    {
        Dead?.Invoke();
    }

    private void OnWeaponPrepared()
    {
        _weapon.TryFire();
    }
}