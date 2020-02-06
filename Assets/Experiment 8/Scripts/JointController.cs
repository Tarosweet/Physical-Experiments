using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] private Jointable jointable;
    private Vector3 startPosition;

    private Transform thisTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thisTransform = transform;
        startPosition = thisTransform.position;
    }

    private void OnMouseDown()
    {
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        if (jointable.isConnected)  // если нужно, чтобы грузики падали когда они не прикреплены = убрать условие
            rb.isKinematic = false;    //TODO отцепить все грузики в цепи
        else
        {
            thisTransform.position = startPosition;
        }
        
        jointable.isConnected = false;
    }
    
}
