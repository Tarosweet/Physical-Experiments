using System;
using System.Collections;
using System.Collections.Generic;
using Experiment_14.Scripts;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Nozzle : MonoBehaviour
{
    [HideInInspector] public Transform nozzleTransform;

    private FixedJoint _fixedJoint;

    public bool IsConnected { get; private set; }

    void Start()
    {
        nozzleTransform = transform;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        var nipple = other.GetComponent<Nipple>();
        
        if (!nipple || IsConnected)
            return;
        
        Connect(nipple);
    }

    private void OnMouseDown()
    {
        if (!IsConnected)
            return;
        
        Disconnect();
    }

    private void Connect(Nipple nipple)
    {
        nozzleTransform.position = nipple.nipplePoint.position;

        _fixedJoint = gameObject.AddComponent<FixedJoint>();
        _fixedJoint.connectedBody = nipple.rigidbody;

        IsConnected = true;
    }

    private void Disconnect()
    {
        Destroy(_fixedJoint);
        IsConnected = false;
    }
}
