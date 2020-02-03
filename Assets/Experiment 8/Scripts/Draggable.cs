using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZPos;

    private Transform dragabbleTransform;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        dragabbleTransform = transform;
    }

    private void OnMouseDown()
    {
        var position = dragabbleTransform.position;
        
        mouseZPos = mainCamera.WorldToScreenPoint(position).z;

        mouseOffset = position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZPos;

        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        dragabbleTransform.position = GetMouseWorldPos() + mouseOffset;
    }
}
