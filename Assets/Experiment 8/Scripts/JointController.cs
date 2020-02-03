using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] private Jointable jointable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        rb.isKinematic = true;
        
        
        if(jointable.isConnected)
            jointable.Unbend();
    }

    private void OnMouseUp()
    {
        if (jointable.isConnected)  // если нужно, чтобы грузики падали когда они не прикреплены = убрать условие
            rb.isKinematic = false;

        jointable.isConnected = false;
    }
    
}
