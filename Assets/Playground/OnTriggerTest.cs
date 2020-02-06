using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerTest : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Triggered");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
    }
}
