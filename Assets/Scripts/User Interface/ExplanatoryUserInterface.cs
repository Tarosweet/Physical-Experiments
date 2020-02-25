using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplanatoryUserInterface : MonoBehaviour
{
    public UnityEvent onTriggered;

    public void Toggle()
    {
        onTriggered?.Invoke();
    }
}
