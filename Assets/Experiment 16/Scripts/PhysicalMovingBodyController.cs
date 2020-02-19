using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalMovingBodyController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float potencialEnergy;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        _rigidbody.isKinematic = true;
    }

    private void OnMouseDrag()
    {
        if (Math.Abs(potencialEnergy - FindObjectOfType<PhysicalMovingBody>().PotentialEnergy) > 0.001f)
        transform.Rotate(Vector3.up, 50 * Time.deltaTime * FindObjectOfType<PhysicalMovingBody>().PotentialEnergy);

        potencialEnergy = FindObjectOfType<PhysicalMovingBody>().PotentialEnergy; //TODO change
    }

    private void OnMouseUp()
    {
        _rigidbody.isKinematic = false;
    }
}
