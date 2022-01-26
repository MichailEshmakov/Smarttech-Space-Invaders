using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInputListener : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        _mover.Move(_input.Player.Moving.ReadValue<Vector2>());
    }
}
