using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorBase : MonoBehaviour
{
    private Transform _transform;
    private bool isPosible = false;

    private void Start()
    {
        _transform = this.transform;
    }

    private void Update()
    {
        Debug.DrawLine(_transform.position -  _transform.forward * 100, _transform.position + _transform.forward * 100, Color.red);
        if (isPosible)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                Rotate(-5);
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                Rotate(5);
            }
        }

    }

    private void Rotate(float y)
    {
        _transform.Rotate(_transform.forward, y);
    }

    private void OnMouseDown()
    {
        isPosible = true;
    }

    private void OnMouseUp()
    {
        isPosible = false;
    }
}
