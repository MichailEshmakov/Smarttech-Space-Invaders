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

    private void Update()
    {
        _mover.Move(Vector2.down);
        _weapon.TryFire();
    }
}
