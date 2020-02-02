using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class HookJoint : MonoBehaviour
{
    public Rigidbody rb;

    private void Start()
    {
      //  joint = GetComponent<HingeJoint>();
       // rb = GetComponent<Rigidbody>();
    }
}
