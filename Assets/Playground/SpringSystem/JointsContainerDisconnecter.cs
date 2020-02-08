using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JointsContainer))]
[RequireComponent(typeof(Draggable))]
public class JointsContainerDisconnecter : MonoBehaviour
{
    [SerializeField] private float distanceToDisconnect = 1f;
    
    private JointsContainer _jointsContainer;
    private Draggable _draggable;

    private bool isConnected;
    void Start()
    {
        _jointsContainer = GetComponent<JointsContainer>();
        _draggable = GetComponent<Draggable>();
    }

    private void OnMouseDown()
    {
       // _jointsContainer.DisconnectMount();
        
        isConnected = _jointsContainer.IsAttached();

    }

    private void OnMouseUp()
    {
        _jointsContainer.mount.collider.enabled = true;

        isConnected = false;
    }

    private void OnMouseDrag()
    {
        if (!isConnected)
            return;

        if ( IsDistanceReached())
            _jointsContainer.DisconnectMount();
    }

    private bool IsDistanceReached()
    {
        return Mathf.Abs(_draggable.GetDistanceFromClick()) > distanceToDisconnect;
    }
}
