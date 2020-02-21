using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadWinder : MonoBehaviour
{
    [SerializeField] private float scaleMultiplierX = 1f;
    [SerializeField] private float scaleMultiplierYZ = 0.5f;
    
    [SerializeField] private PhysicalMovingBody _physicalMovingBody;
    
    private Transform _transform;

    private Vector3 _startScale;

    private void Start()
    {
        _transform = transform;

        _startScale = _transform.localScale;
    }

    private void Update()
    {
        _transform.localScale = GetScaleBasedOnHeight(_startScale, GetHeight(), scaleMultiplierX, scaleMultiplierYZ);
    }

    private float GetHeight()
    {
        return _physicalMovingBody.Height;
    }

    private Vector3 GetScaleBasedOnHeight(Vector3 startScale, float height, float scaleFactorX, float scaleFactorYZ)
    {
        var x = GetAxisValueBasedOnHeight(startScale.x, height, scaleFactorX);
        var y = GetAxisValueBasedOnHeight(startScale.y, height, scaleFactorYZ);
        var z = GetAxisValueBasedOnHeight(startScale.z, height, scaleFactorX);

        return new Vector3(x, y, z);
    }

    private float GetAxisValueBasedOnHeight(float value,float height, float scaleFactor)
    {
        return value * height * scaleFactor;
    }
}
