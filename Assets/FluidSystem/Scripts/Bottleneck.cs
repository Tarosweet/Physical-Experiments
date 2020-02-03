using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottleneck : MonoBehaviour, IPouring
{
    [SerializeField] private float angleToPour = 30;
    
    public Transform pouringObjectTransform;
    void Start()
    {
        pouringObjectTransform = transform;
    }
    
    void Update()
    {
        IsPouring();
    }

    public bool IsPouring()
    {
        return CalculateAngle(pouringObjectTransform.up.y) < angleToPour;
    }
    
    private float CalculateAngle(float rotation)
    {
        return rotation * Mathf.Rad2Deg;
    }
}
