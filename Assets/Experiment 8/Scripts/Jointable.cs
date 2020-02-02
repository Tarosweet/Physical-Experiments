using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jointable : MonoBehaviour
{
    public Rigidbody rb;
    public HingeJoint joint;

    private void OnTriggerEnter(Collider other)
    {
        var joint = other.GetComponent<HookJoint>();
            
        if (!joint)
            return;
        
        Connect(joint);
    }

    private void Connect(HookJoint to)
    {
        joint.connectedBody = to.rb;
    }
}
