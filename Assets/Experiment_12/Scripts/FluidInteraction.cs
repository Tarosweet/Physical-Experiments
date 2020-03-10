using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidInteraction : MonoBehaviour
{
    [SerializeField] private FluidContainer _container;

    private void OnTriggerEnter(Collider other)
    {
        BottomDeflection deflection;
        if (other.transform.TryGetComponent(out deflection))
        {
            deflection.StartInteractWaterLevel(_container);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BottomDeflection deflection;
        if (other.transform.TryGetComponent(out deflection))
        {
            deflection.StopInteractWaterLevel();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        BottomDeflection deflection;
        if (other.transform.TryGetComponent(out deflection))
        {
            deflection.StartInteractWaterLevel(_container);
        }
    }
}
