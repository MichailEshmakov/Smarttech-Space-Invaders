using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Move(Vector3 direction)
    {
        transform.position += direction * _speed * Time.deltaTime;
    }
}
