using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jointable : MonoBehaviour
{
    public Rigidbody rb;
    public HingeJoint joint;

    public bool isConnected;

    private void OnTriggerEnter(Collider other)
    {
        var enteredJoint = other.GetComponent<HookJoint>();
            
        if (!enteredJoint)
           return;
        
        Connect(enteredJoint);
    }

    private void OnTriggerExit(Collider other)
    {
        Unconnect();
    }

    private void Connect(HookJoint to)
    {
        joint.connectedBody = to.rb;
        rb.isKinematic = false;
        isConnected = true;
    }

    public void Unconnect()
    {
        joint.connectedBody = null;
        rb.isKinematic = true;
        isConnected = false;
    }
}
