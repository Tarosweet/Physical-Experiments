using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour
{
    [SerializeField] private Text textComponent;

    [TextArea] 
    [SerializeField] private string text;

    private void Start()
    {
        SetText(text);
    }

    public void SetText(string newText)
    {
        textComponent.text = newText;
    }
}
