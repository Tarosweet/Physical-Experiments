using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jointable : MonoBehaviour
{
    public Rigidbody rb;
    public HingeJoint joint;

    public bool isConnected;

    private RefreshRotation _refreshRotation;

    private void Start()
    {
        _refreshRotation = transform.parent.GetComponent<RefreshRotation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enteredJoint = other.GetComponent<HookJoint>();
            
        if (!enteredJoint)
           return;
        
        Bend(enteredJoint);
    }

    private void OnTriggerExit(Collider other)
    {
          Unbend();
    }

    private void Bend(HookJoint to)
    {
        if (isConnected)
            return;

        joint.connectedBody = to.rb;
        
        EnablePhysics(true);

        SetAnchorPositionsForHingeJoint(to);
        
        Rotate(false);
    }

    public void Unbend()
    {
        if (!isConnected)
            return;
        
        joint.connectedBody = null;
        
        EnablePhysics(false);
        
        Rotate(true);
    }

    private void EnablePhysics(bool value)
    {
        rb.isKinematic = !value;
        isConnected = value;
    }
    
    private void SetAnchorPositionsForHingeJoint(HookJoint to)
    {
        joint.connectedAnchor = to.transform.localPosition;
        joint.anchor = transform.localPosition;
    }

    private void Rotate(bool value)
    {
        if (_refreshRotation)
            _refreshRotation.IsRotating = value;
    }
}
