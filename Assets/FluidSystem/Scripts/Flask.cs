using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speedTranslate;
    private Camera _camera;
    private Transform _cameraTransform;
    private Transform _transform;
    private float _distance;
    private bool _isDrag = false;
    private void Start()
    {
        _transform = this.transform;
        _camera = Camera.main;
        _cameraTransform = _camera.transform;
    }

    private void Update()
    {
        if (_isDrag)
        {
            Move();
        }
    }

    public void Click()
    {
        _isDrag = !_isDrag;
        if(!_isDrag) return;
        
        _distance = Vector3.Distance(_camera.transform.position, _transform.position);
    }

    private void Move()
    {
        _transform.position = Vector3.MoveTowards(_transform.position,
            _cameraTransform.position + _cameraTransform.forward * _distance, Time.deltaTime * _speedTranslate);
    }
}
