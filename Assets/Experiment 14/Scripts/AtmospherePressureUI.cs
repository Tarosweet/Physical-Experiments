using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtmospherePressureUI : MonoBehaviour
{
    [SerializeField] private Text text;

    private int IntText
    {
        get => Convert.ToInt32(text.text);
        set => text.text = value.ToString();
    }

    [SerializeField] private AtmospherePressure _atmospherePressure;

    [SerializeField] private float lerpDuration = 2f;

    void Update()
    {
        LerpInt();
    }

    private void LerpInt()
    {
        var lerp = Time.deltaTime / lerpDuration;
        var value = (int) Mathf.SmoothStep(IntText, _atmospherePressure.force, lerp);

        IntText = value;
    }
    
}
