using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public float Speed => _speed;

    public void Move(Vector3 direction)
    {
        direction.Normalize();
        transform.position += direction * _speed * Time.deltaTime;
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }
}
