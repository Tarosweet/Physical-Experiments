using System;
using Extensions;
using Helpers;
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

        public bool isActive;

        public float Distance => slider.value;

        [SerializeField] private Transform target;
        
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        [SerializeField] private VectorExtension.Axis distanceAxis = VectorExtension.Axis.X;
        [SerializeField] private float distanceMultiplier = 2f;

        [SerializeField] private Slider slider;
        [SerializeField] private Text text;

        [SerializeField] private bool calculateOneDirection = true;
 
        [SerializeField] private Debug debug;

        private void Start()
        {
            InitializeSliderValues();
            
            if (isActive)
                MoveSlider();
        }

        private void OnValidate()
        {
            InitializeSliderValues();
        }

        private void InitializeSliderValues()
        {
                slider.minValue = Mathf.Abs(VectorExtension.GetAxisValueFromVector(startPoint.position, distanceAxis));
                slider.maxValue = Mathf.Abs(VectorExtension.GetAxisValueFromVector(endPoint.position, distanceAxis));
        }

        private void Update()
        {
            if (isActive)
                MoveSlider();
        }

        private void MoveSlider()
        {
            slider.value =  Mathf.Abs(VectorExtension.GetAxisValueFromVector(target.position, distanceAxis));
            text.text = GetDistance(target.position, startPoint.position).ToString("F1") + Units.Distance.Meters;
        }

        private float GetDistance(Vector3 a, Vector3 b)
        {
            var distance = VectorExtension.GetAxisValueFromVector(a,VectorExtension.Axis.X) 
                           - VectorExtension.GetAxisValueFromVector(b, distanceAxis);
            if (calculateOneDirection)
                return Mathf.Clamp(distance, 0, VectorExtension.GetAxisValueFromVector(endPoint.position, distanceAxis) 
                                            * distanceMultiplier);
            return Mathf.Abs(distance);
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
