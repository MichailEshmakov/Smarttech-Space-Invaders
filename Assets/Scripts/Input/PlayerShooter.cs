using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private InputListener _input;

    private void OnValidate()
    {
        if (_weapon == null)
            _weapon = GetComponent<Weapon>();

        if (_input == null)
            _input = GetComponent<InputListener>();
    }

    private void OnEnable()
    {
        _input.FireButtonClick += OnFireButtonClick;
    }

    private void OnFireButtonClick()
    {
        _weapon.TryFire();
    }
}
