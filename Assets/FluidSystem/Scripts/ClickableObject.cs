using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject: MonoBehaviour
{
    public Action OnButtonDown;
    public Action OnButtonUp;
    private void OnMouseDown()
    {
        if (OnButtonDown != null) 
            OnButtonDown.Invoke();
    }

    private void OnMouseUp()
    {
        if (OnButtonUp != null) 
            OnButtonUp.Invoke();
    }
}
