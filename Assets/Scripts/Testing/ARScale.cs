using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARScale : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform ar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var value = slider.value;
        ar.localScale = new Vector3(value,value,value);
    }
}
