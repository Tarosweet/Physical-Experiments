using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour , IPouring
{
    private Transform holeTransform;
    void Start()
    {
        holeTransform = transform;
    }
    
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(holeTransform.position, 0.5f);
    }

    public bool IsPouring()
    {
        return true;
    }
}
