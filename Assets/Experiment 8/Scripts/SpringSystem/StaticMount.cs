using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StaticMount : MonoBehaviour
{
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.isKinematic = true;
    }
    
}
