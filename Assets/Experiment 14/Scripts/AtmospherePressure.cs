using System;
using System.Collections;
using System.Collections.Generic;
using Experiment_14.Scripts;
using UnityEngine;

public class AtmospherePressure : MonoBehaviour, IPumped
{
    [Serializable]
    public class AtmospherePressureBetween
    {
        public Transform firstObject;
        public Transform secondObject;

        [SerializeField] private float distanceForAtmosphericPressureToWork = 2.5f;
        [SerializeField] private float angleForAtmosphericPressureToWork = 0.5f;

        public bool IsNear()
        {
            Debug.Log(Vector3.Distance(firstObject.position, secondObject.position));
            return Vector3.Distance(firstObject.position, secondObject.position) < distanceForAtmosphericPressureToWork;
        }

        public bool IsLookingOnEachOther()
        {
            Debug.Log(Mathf.Abs(Vector3.Angle(firstObject.forward, secondObject.forward)));
            return Mathf.Abs(Vector3.Angle(firstObject.forward, secondObject.forward)) <
                   angleForAtmosphericPressureToWork;
        }
    }
    
    [SerializeField] private AtmospherePressureBetween _atmospherePressureBetween;
    
    [SerializeField] private float forceToPress;
    
    public float force;

    private FixedJoint _fixedJoint;

    public void Pumped(float value)
    {
        if (!_atmospherePressureBetween.IsLookingOnEachOther() || !_atmospherePressureBetween.IsNear())
            return;
        
        force += value;
        
        if (IfNeedToPress())
            Press();
    }
    
    private bool IfNeedToPress()
    {
        return force > forceToPress;
    }

    private void Press()
    {
        if (IsPressed())
            return;
        
        CreateConnection();
    }

    private void CreateConnection()
    {
        _fixedJoint = _atmospherePressureBetween.firstObject.gameObject.AddComponent<FixedJoint>();
        _fixedJoint.connectedBody = _atmospherePressureBetween.secondObject.GetComponent<Rigidbody>();
    }

    private void Unpress()
    {
        Destroy(_fixedJoint);
    }

    private bool IsPressed()
    {
        return _fixedJoint;
    }
}
