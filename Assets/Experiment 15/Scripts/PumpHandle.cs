using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpHandle : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _upRotation;
    [SerializeField] private Vector3 _downRotation;

    [SerializeField] private float _percent;

    private Vector3 _startRayPosition;
    private Vector3 _currentRayPosition;

    public Action<float> onHandUp;
    public Action<float> onHandDown;
    
    private void OnMouseDown()
    {
        _startRayPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        _currentRayPosition = _startRayPosition;
    }

    private void OnMouseDrag()
    {
        _currentRayPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        Vector3 direction = (_currentRayPosition - _startRayPosition).normalized;
        if(direction.y == 0) return;

        if (direction.y > 0)
        {
            HandUp();
        }
        else
        {
            HandDown();
        }

        _startRayPosition = _currentRayPosition;
    }
    
    private void HandUp()
    {
        Quaternion to = Quaternion.Euler(_upRotation);
        _transform.localRotation = Quaternion.RotateTowards(_transform.localRotation, to, _speed);
        CalculatePercent();
        
        onHandUp?.Invoke(_percent);
    }
    
    private void HandDown()
    {
        Quaternion to = Quaternion.Euler(_downRotation);
        _transform.localRotation = Quaternion.RotateTowards(_transform.localRotation, to, _speed);
        CalculatePercent();
        
        onHandDown?.Invoke(_percent);
    }

    private void CalculatePercent()
    {
        Quaternion up = Quaternion.Euler(_upRotation);
        Quaternion down = Quaternion.Euler(_downRotation);
        float maxAngle = Quaternion.Angle(up, down);
        float angle = Quaternion.Angle(_transform.localRotation, up);
        _percent = angle / maxAngle;
    }
}
