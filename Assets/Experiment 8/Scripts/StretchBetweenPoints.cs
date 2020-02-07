using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchBetweenPoints : MonoBehaviour
{
    private Transform springTransform;

    public Transform firstPointTransform;
    public Transform secondPointTransform;

    [SerializeField] private float XZScale;
    
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
        float distance = Vector3.Distance(firstPoint, secondPoint); //Change Scale
        springTransform.localScale = new Vector3(XZScale, XZScale, distance);

        Vector3 middlePoint = (firstPoint + secondPoint) / 2; //Change Position
        springTransform.position = middlePoint;
        
        Vector3 rotationDirection = (secondPoint - firstPoint); //Change Rotation
        springTransform.rotation = Quaternion.LookRotation(rotationDirection);
    }
}
