using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private Jointable jointable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        rb.isKinematic = true;
        
        if(jointable.IsConnected())
            jointable.Unconnect();
    }

    private void OnMouseUp()
    {
        if (jointable.IsConnected())  // если нужно, чтобы грузики падали когда они не прикреплены = убрать условие
            rb.isKinematic = false;
    }
}
