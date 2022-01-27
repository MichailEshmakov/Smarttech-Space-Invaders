using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private InputListener _input;

    private void OnValidate()
    {
        if (_mover == null)
            _mover = GetComponent<Mover>();

        if (_input == null)
            _input = GetComponent<InputListener>();
    }

    private void Update()
    {
        _mover.Move(_input.MovingDirection);
    }
}
