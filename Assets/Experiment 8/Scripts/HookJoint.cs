using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class HookJoint : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hook"))
            Joint(other.gameObject);
            
    }

    private void Joint(GameObject to)
    {
        HingeJoint joint = to.AddComponent<HingeJoint>();
        InitializeJoint(joint);
    }

    private void InitializeJoint(HingeJoint joint)
    {
        joint.connectedBody = GetComponent<Rigidbody>();
    }
}
