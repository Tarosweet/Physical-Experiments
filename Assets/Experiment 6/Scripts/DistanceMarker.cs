using System;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Experiment_6.Scripts
{
    public class DistanceMarker : MonoBehaviour
    {
        [Serializable]
        private struct Debug
        {
            public Vector3 pointsSize;
            public Color startPointColor;
            public Color endPointColor;
        }

        [SerializeField] private Transform target;
        
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        [SerializeField] private VectorExtension.Axis distanceAxis = VectorExtension.Axis.X;

        [SerializeField] private Slider slider;

        [SerializeField] private Debug debug;
    
        private void Update()
        {
            slider.minValue = VectorExtension.GetAxisValueFromVector(startPoint.position, distanceAxis);
            slider.maxValue = VectorExtension.GetAxisValueFromVector(endPoint.position, distanceAxis);

            slider.value = VectorExtension.GetAxisValueFromVector(target.position, distanceAxis);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = debug.startPointColor;
            Gizmos.DrawCube(startPoint.position,debug.pointsSize);
            
            Gizmos.color = debug.endPointColor;
            Gizmos.DrawCube(endPoint.position,debug.pointsSize);
        }
    }
}
