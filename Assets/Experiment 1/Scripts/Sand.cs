using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sand : MonoBehaviour
{

    public float strength = 0.05f;
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        var rigidbodyInSand = other.GetComponent<Rigidbody>();
        
        if (!rigidbodyInSand)
            return;
        
        StopRigidbody(rigidbodyInSand);
    }

    private void StopRigidbody(Rigidbody rigidbody)
    {
        Vector3 newVelocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x - strength,0,10),
            Mathf.Clamp(rigidbody.velocity.y - strength,0,10),
            Mathf.Clamp(rigidbody.velocity.z - strength,0,10));

        rigidbody.velocity = newVelocity;
        
        Debug.Log("Velocity: " + newVelocity);
    }
}
