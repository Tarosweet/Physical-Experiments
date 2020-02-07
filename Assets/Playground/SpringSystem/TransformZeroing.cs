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

    private void OnMouseUp() //вынести в другой класс
    {
        if (_jointController.IsAttached()) 
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
