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

    void Start()
    {
        _transform = transform;

        Save();
    }

    private void Save()
    {
        _position = _transform.position;
        _rotation = _transform.rotation;
    }

    public void Load()
    {
        _transform.position = _position;
        _transform.rotation = _rotation;
    }
}
