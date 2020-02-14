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
        _transform.Rotate(new Vector3(0, 0, y));
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
