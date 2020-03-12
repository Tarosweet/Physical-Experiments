using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpPiston : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private PumpHandle _pumpHandle;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;
    
    private void OnEnable()
    {
        _pumpHandle.onHandDown += Move;
        _pumpHandle.onHandUp += Move;
    }

    private void OnDisable()
    {
        _pumpHandle.onHandDown -= Move;
        _pumpHandle.onHandUp -= Move;
    }

    private void Move(float percent)
    {
        Vector3 direction = (_endPosition - _startPosition).normalized;
        float distance = Vector3.Distance(_startPosition, _endPosition);
        Vector3 position = _startPosition + direction * (distance * percent);
        _transform.localPosition = position;
    }
}
