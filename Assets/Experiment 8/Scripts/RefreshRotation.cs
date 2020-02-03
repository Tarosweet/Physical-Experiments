using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshRotation : MonoBehaviour
{
    [SerializeField] private Vector3 refreshingRotation;
    [SerializeField] private bool autoGetRefreshingRotation;

    [SerializeField] private float timeToRefresh = 3f;
    
    private Transform rotatingTransform;
    
    public bool IsRotating { set; get; }
    void Start()
    {
        rotatingTransform = transform;
        
        if (autoGetRefreshingRotation)
            refreshingRotation = rotatingTransform.rotation.eulerAngles;
    }

    void Update()
    {
        if (IsRotating)
            Rotate();

        if (IsRotationReached(rotatingTransform.rotation.eulerAngles, refreshingRotation))
            IsRotating = false;
    }

    private void Rotate()
    {
        rotatingTransform.rotation = Quaternion.Slerp(rotatingTransform.rotation, Quaternion.Euler(refreshingRotation)
            ,timeToRefresh * Time.deltaTime);
    }

    private bool IsRotationReached(Vector3 firstRotation, Vector3 secondRotation)
    {
        return Vector3.Angle(firstRotation, secondRotation) < Mathf.Epsilon;
    }
}
