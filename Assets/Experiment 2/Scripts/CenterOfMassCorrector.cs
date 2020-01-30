using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMassCorrector : MonoBehaviour
{
    [SerializeField] private Vector3 centerOfMass;
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
    }
}
