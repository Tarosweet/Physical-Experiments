using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxwellPendulum : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    [SerializeField] private Transform _transform;

    [SerializeField] private Transform endPoint;

    private bool isEnd;

    private void Update()
    {
        float E = rigidbody.mass * Mathf.Pow(rigidbody.velocity.magnitude, 2) * 0.5f;
        Debug.Log("Kinematic: " + E);

        float Ep = -Physics.gravity.y * rigidbody.mass * (_transform.position - endPoint.position).magnitude;
        Debug.Log("Potencial: " + Ep);
        
        Debug.Log("Velocity: " +rigidbody.velocity);

        if (Ep < 1 && !isEnd)
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y * (-1));
            isEnd = true;
        }

        if (Input.anyKey)
            isEnd = false;
        
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y,-20,20f));
    }
}
