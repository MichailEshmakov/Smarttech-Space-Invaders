using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputListener : MonoBehaviour
{
    private PlayerInput _input;

    public Vector2 MovingDirection => _input.Player.Moving.ReadValue<Vector2>();

    public event UnityAction FireButtonClick;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Fire.performed += OnFirePerformed;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Fire.performed -= OnFirePerformed;
    }

    private void OnFirePerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        FireButtonClick?.Invoke();
    }
}
