using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LockRotation
{
    public bool x, y, z;

    public Vector3 Lock(Vector3 rotation)
    {
        if (x) rotation.x = 0;
        if (y) rotation.y = 0;
        if (z) rotation.z = 0;

        return rotation;
    }
}

public class StretchBetweenPoints : MonoBehaviour
{
    private Transform springTransform;

    public Transform firstPointTransform;
    public Transform secondPointTransform;

    [SerializeField] private float XZScale;

    [SerializeField] private LockRotation _lockRotation;
    
    void Start()
    {
        springTransform = transform;
    }
    
    void Update()
    {
        if (!firstPointTransform || !secondPointTransform)
            return;
        
        Stretch(firstPointTransform.position,secondPointTransform.position);
    }

    private void Stretch(Vector3 firstPoint, Vector3 secondPoint)
    {
        float distance = Vector3.Distance(firstPoint, secondPoint); 
        springTransform.localScale = new Vector3(XZScale, XZScale, distance);

        Vector3 middlePoint = (firstPoint + secondPoint) / 2; 
        springTransform.position = middlePoint;
        
        Vector3 rotationDirection = (secondPoint - firstPoint); 
        rotationDirection = _lockRotation.Lock(rotationDirection);
        springTransform.rotation = Quaternion.LookRotation(rotationDirection);
    }
}
