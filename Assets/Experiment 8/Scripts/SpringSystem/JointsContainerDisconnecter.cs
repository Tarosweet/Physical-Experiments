using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Draggable))]
[RequireComponent(typeof(BoxCollider))]
public class JointsContainerDisconnecter : MonoBehaviour
{
    [SerializeField] private float distanceToDisconnect = 1f;
    
    [SerializeField] private JointsContainer _jointsContainer;
    private Draggable _draggable;

    private bool isConnected;
    void Start()
    {
        _draggable = GetComponent<Draggable>();
        
        SetJointContainer();
    }

    private void SetJointContainer()
    {
        if (!_jointsContainer)
        {
            _jointsContainer = GetComponent<JointsContainer>();
            Debug.LogWarning("In JCDisconnecter joints container not set .Container property set to self.");
        }
    }

    private void OnMouseDown()
    {
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
