using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    private void OnValidate()
    {
        if (_mover == null)
            _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        _mover.Move(Vector2.down);
    }
}
