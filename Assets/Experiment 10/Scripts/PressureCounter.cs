using System;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Experiment_10.Scripts
{
    public class PressureCounter : MonoBehaviour
    {
        [SerializeField] private Transform minTransform;
        [SerializeField] private Transform handle;
        [SerializeField] private Vector3 maxHandle;

        [SerializeField] private float minPressure = 2;
        [SerializeField] private float maxPressure = 10;

        [SerializeField] private Text text;

        private void Start()
        {
            maxHandle = handle.position;
        }

        private void Update()
        { 
            text.text = CalculatePressure().ToString() + Units.Pressure.Pascal;
        }

        private float CalculatePressure()
        {
            return maxPressure + (handle.position.y - minTransform.position.y) * (maxPressure - minPressure) /
                   (maxHandle.y - minTransform.position.y);
        }
    }
}
