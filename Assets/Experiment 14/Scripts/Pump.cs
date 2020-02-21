using System;
using System.Collections;
using System.Collections.Generic;
using Experiment_14.Scripts;
using Extensions;
using UnityEngine;

public class Pump : MonoBehaviour
{
    private Transform _transform;
    
     private Vector3 _startPosition;
     
     private Vector3 _lastPosition;

     private PumpBehavior _currentPumpBehavior;

     [SerializeField] private Nozzle nozzle;

     [SerializeField] private AtmospherePressure _atmospherePressure;

     [SerializeField] private float distanceToPump = 0.3f;
     
     
    void Start()
    {
        _transform = transform;
        
        _startPosition = _transform.position;

        _lastPosition = _startPosition;
    }

    private void Update()
    {
        _currentPumpBehavior = GetState(
            VectorExtension.DirectionVectorNormalized(_transform.position, _lastPosition));

       if (VectorExtension.IsPassedDistanceInDirection(_transform.position, 
            _lastPosition,VectorExtension.Axis.Y,distanceToPump))
        {
            if (nozzle.IsConnected)
            {
                _currentPumpBehavior.Pump(_atmospherePressure);
            }

            _lastPosition = _transform.position;
        }
    }

    private PumpBehavior GetState(Vector3 direction)
    {
        if (direction.y < -0.1f)
            return new PumpDown();

        if (direction.y > 0.1f)
            return new PumpUp();

        return new PumpDown();
    }
}
