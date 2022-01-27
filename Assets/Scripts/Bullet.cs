using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Mover _mover;

    private Vector2 _derection;

    private void OnValidate()
    {
        if (_mover == null)
            _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        _mover.Move(_derection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    public void Init(Vector2 direction)
    {
        _derection = direction;
        _derection.Normalize();
    }
}
