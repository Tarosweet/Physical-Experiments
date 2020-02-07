using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JointsContainer))]
public class JointsContainerDisconnecter : MonoBehaviour
{
    private JointsContainer _jointsContainer;
    void Start()
    {
        _jointsContainer = GetComponent<JointsContainer>();
    }

    private void OnMouseDown()
    {
        _jointsContainer.DisconnectMount();
    }
}
