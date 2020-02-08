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

    public Mount currentMount;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void BeDisconnect()
    {
        currentMount = null;
    }

    public bool IsAttached()
    {
        return currentMount;
    }

    private void OnTriggerEnter(Collider other)
    {
        Mount mount = other.GetComponent<Mount>();

        if (!mount)
            return;
        

        if (mount.IsAttached())
            return;
        
        Debug.Log("WADS");
        
        BeConnect(mount);
    }

    private void BeConnect(Mount mount)
    {
        currentMount = mount;
        
        mount.Connect(jointsContainer);
    }
}
