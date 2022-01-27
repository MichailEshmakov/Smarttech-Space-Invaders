using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _topBorder;
    [SerializeField] private Transform _bottomBorder;

    public float RightBorderCoordinate => _rightBorder.position.x;
    public float LeftBorderCoordinate => _leftBorder.position.x;
    public float TopBorderCoordinate => _topBorder.position.y;
    public float BottomBorderCoordinate => _bottomBorder.position.y;
}
