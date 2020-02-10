using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetHandle : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _handle;
    [SerializeField] private Vector3 _toRotate;
    [SerializeField] private Vector3 _startRotate;
    [SerializeField] private ClickableObject _clickable;
    [SerializeField] private float _speed;
    private bool _isActive = false;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _clickable.OnButtonDown += Turn;
    }
    private void OnDisable()
    {
        _clickable.OnButtonDown -= Turn;
    }

    private void TurnOn()
    {
        _coroutine = StartCoroutine(Turn(_toRotate));
    }

    private void TurnOff()
    {
        _coroutine = StartCoroutine(Turn(_startRotate));
    }

    private void Turn()
    {
        _isActive = !_isActive;
        
        if(_coroutine != null)
            StopCoroutine(_coroutine);
        
        if (_isActive)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    private IEnumerator Turn(Vector3 toPosition)
    {
        Quaternion newRotation = Quaternion.Euler(toPosition);
        float angle = Quaternion.Angle(_handle.localRotation, newRotation);
        while (angle > Mathf.Epsilon)
        {
            _handle.localRotation = Quaternion.RotateTowards(_handle.localRotation, newRotation,Time.deltaTime * _speed);
            angle = Quaternion.Angle(_handle.localRotation, newRotation);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}
