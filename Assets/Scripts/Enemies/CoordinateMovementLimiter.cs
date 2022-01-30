using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateMovementLimiter : MonoBehaviour
{
    [SerializeField] private CoordinateLitera _litera;
    [SerializeField] private bool _isUpperLimit;
    [SerializeField] private float _limit;

    private Func<float, float, bool> _isOverLimit;

    private void Start()
    {
        _isOverLimit = GetIsOverLimit();
    }

    private void Update()
    {
        float currentCoordinate = GetCurrentCoordinate();
        if (_isOverLimit(_limit, currentCoordinate))
            SetLimitCoordinate();
    }

    public void Init(CoordinateLitera litera, bool isUpperLimit, float limit)
    {
        _litera = litera;
        _isUpperLimit = isUpperLimit;
        _limit = limit;
    }

    private float GetCurrentCoordinate()
    {
        if (_litera == CoordinateLitera.x)
            return transform.position.x;
        else
            return transform.position.y;
    }

    private void SetLimitCoordinate()
    {
        if (_litera == CoordinateLitera.x)
            transform.position = new Vector3(_limit, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, _limit, transform.position.z);
    }

    private Func<float, float, bool> GetIsOverLimit()
    {
        if (_isUpperLimit)
            return (float limit, float value) => limit < value;
        else
            return (float limit, float value) => limit > value;
    }
}

public enum CoordinateLitera
{
    x,
    y
}
