using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nipple : MonoBehaviour
{
    public Transform nipplePoint;
    
    [HideInInspector] public Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
}
