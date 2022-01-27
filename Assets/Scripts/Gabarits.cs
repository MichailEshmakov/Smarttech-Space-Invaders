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
        if (_leftExtremePoint != null && _rightExtremePoint != null)
            CheckXSides();

        if (_topExtremePoint != null && _bottomExtremePoint != null)
            CheckYSides();
    }

    private void Start()
    {
        CheckXSides();
        CheckYSides();
        SetSizes();
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
