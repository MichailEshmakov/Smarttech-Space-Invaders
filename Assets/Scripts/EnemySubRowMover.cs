using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class EnemySubRowMover : MonoBehaviour
{
    private Mover _mover;
    private Vector2 _movingDirection;

    public Vector2 MovingDirection => _movingDirection;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        _mover.Move(_movingDirection);
    }

    public void Init(float speed, Vector2 movingDirection)
    {
        _mover.SetSpeed(speed);
        _movingDirection = movingDirection;
    }

    public void SetSpeed(float speed)
    {
        _mover.SetSpeed(speed);
    }

    public void ChangeDirection()
    {
        _movingDirection *= -1;
    }
}
