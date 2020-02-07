using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JointsContainer))]
public class TransformZeroing : MonoBehaviour
{
    private Transform _transform;

    private Vector3 _position;

    private Quaternion _rotation;

    private JointsContainer _jointController; //вынести
    
    void Start()
    {
        _transform = transform;

        _jointController = GetComponent<JointsContainer>();
        
        Save();
    }

    private void OnMouseUp() //TODO вынести в другой класс
    {
        if (_jointController.IsAttached())
        {
            _jointController.rigidbody.isKinematic = false;
            return;
        }
        
        if (_jointController.IsHaveAttaches())
            return;

        Load();

        _jointController.rigidbody.isKinematic = true;
    }

    private void Save()
    {
        _position = _transform.position;
        _rotation = _transform.rotation;
    }

    private void Load()
    {
        _transform.position = _position;
        _transform.rotation = _rotation;
    }
}
