using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(SphereCollider))]
public class Hook : MonoBehaviour
{
    public Rigidbody rigidbody;

    public JointsContainer jointsContainer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Mount mount = other.GetComponent<Mount>();

        if (!mount)
            return;
        
        mount.Connect(jointsContainer);
    }
}
