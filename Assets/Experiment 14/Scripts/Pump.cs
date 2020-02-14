using System;
using System.Collections;
using System.Collections.Generic;
using Experiment_14.Scripts;
using UnityEngine;

public class Pump : MonoBehaviour
{
     private Transform _transform;
    
     private Vector3 _startPosition;
     
     private Vector3 _lastPosition;

     private PumpBehavior _currentPumpBehavior;

     [SerializeField] private AtmospherePressure _atmospherePressure;
    
    void Start()
    {
        _transform = transform;
        
        _startPosition = _transform.position;

        _lastPosition = _startPosition;
    }

    private void Update()
    {
        _currentPumpBehavior = GetState(DirectionVector(_transform.position, _lastPosition));

        _lastPosition = _transform.position;

        if (IsWentDistance())
        {
            _currentPumpBehavior.Pump(_atmospherePressure);
        }
    }

    private Vector3 DirectionVector(Vector3 currentPosition, Vector3 lastPosition)
    {
        return (currentPosition - lastPosition).normalized;
    }

    private PumpBehavior GetState(Vector3 direction)
    {
        if (direction.y < -0.1f)
            return new PumpDown();

        if (direction.y > 0.1f)
            return new PumpUp();

        return new PumpUp();
    }

    private bool IsWentDistance()
    {
        return Math.Abs((_transform.position.y % 1.2f)) < 0.4f;
    }
}
