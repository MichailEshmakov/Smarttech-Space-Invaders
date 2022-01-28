using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gabarits : MonoBehaviour
{
    [SerializeField] private Transform _leftExtremePoint;
    [SerializeField] private Transform _rightExtremePoint;
    [SerializeField] private Transform _topExtremePoint;
    [SerializeField] private Transform _bottomExtremePoint;

    private float _leftWidth;
    private float _rightWidth;
    private float _topHeight;
    private float _bottomHeight;

    public float LeftWidth => _leftWidth;
    public float RightWidth => _rightWidth;
    public float TopHeight => _topHeight;
    public float BottomHeight => _bottomHeight;

    public float LeftExtremeCoordinate => _leftExtremePoint.position.x;
    public float RightExtremeCoordinate => _rightExtremePoint.position.x;
    public float TopExtremeCoordinate => _topExtremePoint.position.y;
    public float BottomExtremeCoordinate => _bottomExtremePoint.position.y;

    private void OnValidate()
    {
        if (IsAnyExtremePointNull() == false)
        {
            CheckXSides();
            CheckYSides();
        }
    }

    private void Start()
    {
        if (IsAnyExtremePointNull() == false)
        {
            CheckXSides();
            CheckYSides();
            SetSizes();
        }
    }

    public void Init(Transform leftExtremePoint, Transform rightExtremePoint, Transform topExtremePoint, Transform bottomExtremePoint)
    {
        _leftExtremePoint = leftExtremePoint;
        _rightExtremePoint = rightExtremePoint;
        _topExtremePoint = topExtremePoint;
        _bottomExtremePoint = bottomExtremePoint;
    }

    public void Init(float leftExtremeCoordinate, float rightExtremeCoordinate, float topExtremeCoordinate, float bottomExtremeCoordinate)
    {
        Transform leftExtremePoint = CreateExtremePoint(nameof(leftExtremePoint), new Vector2(leftExtremeCoordinate, transform.position.y));
        Transform rightExtremePoint = CreateExtremePoint(nameof(rightExtremePoint), new Vector2(rightExtremeCoordinate, transform.position.y));
        Transform topExtremePoint = CreateExtremePoint(nameof(topExtremePoint), new Vector2(transform.position.x, topExtremeCoordinate));
        Transform bottomExtremePoint = CreateExtremePoint(nameof(bottomExtremePoint), new Vector2(transform.position.x, bottomExtremeCoordinate));

        Init(leftExtremePoint, rightExtremePoint, topExtremePoint, bottomExtremePoint);
    }

    private Transform CreateExtremePoint(string name, Vector2 position)
    {
        Transform newPoint = new GameObject(name).transform;
        newPoint.SetParent(transform);
        newPoint.position = position;

        return newPoint;
    }

    private void CheckXSides()
    {
        if (LeftExtremeCoordinate > RightExtremeCoordinate)
        {
            LogEggorOfWrongPosition(nameof(_leftExtremePoint));
            LogEggorOfWrongPosition(nameof(_rightExtremePoint));
        }  
    }

    private void CheckYSides()
    {
        if (TopExtremeCoordinate < BottomExtremeCoordinate)
        {
            LogEggorOfWrongPosition(nameof(_topExtremePoint));
            LogEggorOfWrongPosition(nameof(_bottomExtremePoint));
        }
    }

    private bool IsAnyExtremePointNull()
    {
        return (_leftExtremePoint == null)
            || (_rightExtremePoint == null)
            || (_topExtremePoint == null)
            || (_bottomExtremePoint == null);
    }

    private void LogEggorOfWrongPosition(string sideName)
    {
        Debug.LogError($"Wrong position of {sideName}");
    }

    private void SetSizes()
    {
        _leftWidth = transform.position.x - _leftExtremePoint.position.x;
        _rightWidth = _rightExtremePoint.position.x - transform.position.x;
        _topHeight = _topExtremePoint.position.y - transform.position.y;
        _bottomHeight = transform.position.y - _bottomExtremePoint.position.y;
    }
}
