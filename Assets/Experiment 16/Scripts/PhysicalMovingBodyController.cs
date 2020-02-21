using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovingBodyRotation))]
[RequireComponent(typeof(Rigidbody))]
public class PhysicalMovingBodyController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MovingBodyRotation _movingBodyRotation;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movingBodyRotation = GetComponent<MovingBodyRotation>();
    }

    private void OnMouseDown()
    {
        _rigidbody.isKinematic = true;
    }

    private void OnMouseDrag()
    {
        _movingBodyRotation.RotateWithPlayerContolls();
    }

    private void OnMouseUp()
    {
        _rigidbody.isKinematic = false;
    }
}
