using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovmentLimiter : MonoBehaviour
{
    [SerializeField] private Borders _borders;
    [SerializeField] private Gabarits _gabarits;

    private void OnValidate()
    {
        if (_borders == null)
            _borders = FindObjectOfType<Borders>();

        if (_gabarits == null)
            _gabarits = GetComponentInChildren<Gabarits>();
    }

    private void LateUpdate()
    {
        if (_gabarits.LeftExtremeCoordinate < _borders.LeftBorderCoordinate)
            transform.position = new Vector3(_borders.LeftBorderCoordinate + _gabarits.LeftWidth, transform.position.y, transform.position.z);

        if (_gabarits.RightExtremeCoordinate > _borders.RightBorderCoordinate)
            transform.position = new Vector3(_borders.RightBorderCoordinate - _gabarits.RightWidth, transform.position.y, transform.position.z);

        if (_gabarits.TopExtremeCoordinate > _borders.TopBorderCoordinate)
            transform.position = new Vector3(transform.position.x, _borders.TopBorderCoordinate - _gabarits.TopHeight, transform.position.z);

        if (_gabarits.BottomExtremeCoordinate < _borders.BottomBorderCoordinate)
            transform.position = new Vector3(transform.position.x, _borders.BottomBorderCoordinate + _gabarits.BottomHeight, transform.position.z);
    }
}
