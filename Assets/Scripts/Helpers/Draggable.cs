using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Draggable : MonoBehaviour
{
    
    private Vector3 mouseOffset;
    private float mouseZPos;

    [SerializeField] private Transform dragabbleTransform;
    
    [SerializeField] private float speed = 10f;
    
    private Camera mainCamera;

    private Vector3 position;

    private bool _isDrag;

    private void Start()
    {
        mainCamera = Camera.main;

        SetTarget();
    }

    private void SetTarget()
    {
        if (!dragabbleTransform)
            dragabbleTransform = transform;
    }

    private void OnMouseDown()
    {
        position = dragabbleTransform.position;

        mouseZPos = mainCamera.WorldToScreenPoint(position).z;

        mouseOffset = position - GetMouseWorldPos();
        
        _isDrag = true;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZPos;

        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseUp()
    {
        _isDrag = false;
    }

    private void FixedUpdate()
    {
        if (!_isDrag)
            return;
        
        Drag();
    }

    private void Drag()
    {
        dragabbleTransform.position = Vector3.Lerp(dragabbleTransform.transform.position,
            GetMouseWorldPos() + mouseOffset,speed*Time.deltaTime);
    }

    public float GetDistanceFromClick()
    {
        return position.x - GetMouseWorldPos().x;
    }
    
}
