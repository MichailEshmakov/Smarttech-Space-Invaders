using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Weapon _weapon;

    private void OnValidate()
    {
        if (_mover == null)
            _mover = GetComponent<Mover>();


        if (_weapon == null)
            _weapon = GetComponent<Weapon>();
    }

    private void OnEnable()
    {
        _weapon.ToFirePrepared += OnWeaponPrepared;
    }

    private void OnDisable()
    {
        _weapon.ToFirePrepared -= OnWeaponPrepared;
    }

    private void OnWeaponPrepared()
    {
        _weapon.TryFire();
    }

    private void Update()
    {
        _mover.Move(Vector2.down);
    }
}
