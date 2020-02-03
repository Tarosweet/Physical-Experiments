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
        rb.isKinematic = false;
        isConnected = true;

        joint.connectedAnchor = to.transform.localPosition;
        joint.anchor = transform.localPosition;
        
        _refreshRotation.IsRotating = false;

      /*  if (to.transform.parent.GetComponent<HingeJoint>().axis.x == 0)
            joint.axis = new Vector3(1,1);
        else
        {
            joint.axis = new Vector3(0,1);
        } */

    }

    public void Unbend()
    {
        if (!isConnected)
            return;
        
        joint.connectedBody = null;
        rb.isKinematic = true;
        isConnected = false;
        
        _refreshRotation.IsRotating = true;
    }
}
