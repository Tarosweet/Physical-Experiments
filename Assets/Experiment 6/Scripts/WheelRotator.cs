using System;
using System.Collections.Generic;
using UnityEngine;

namespace Experiment_6.Scripts
{
    public class WheelRotator : MonoBehaviour
    {
        [SerializeField] private List<Transform> meshs = new List<Transform>();
        [SerializeField] private List<WheelCollider> wheelColliders = new List<WheelCollider>();

        [SerializeField] private float speed = 60;

        [SerializeField] private Dictionary<Transform, WheelCollider> wheelsDictionary = new Dictionary<Transform, WheelCollider>();

        private const float MaxAngle = 360;

        private void Start()
        {
            for (var i = 0; i < meshs.Count; i++)
            {
                wheelsDictionary.Add(meshs[i], wheelColliders[i]);
            }
        }

        private void OnValidate()
        {
            if (meshs.Count != wheelColliders.Count)
                Debug.LogError("Wheel mesh count not equal to wheel colliders!");
        }

        private void Update()
        {
            RotateAllWheels(wheelsDictionary);
        }

        private void RotateAllWheels(Dictionary<Transform,WheelCollider> wheels)
        {
            foreach (var wheel in wheels)
            {
                Rotate(wheel.Key, wheel.Value);
            }
        }

        private void Rotate(Transform mesh, WheelCollider wheelCollider)
        {
            mesh.Rotate(wheelCollider.rpm / speed * MaxAngle * Time.deltaTime, 0,0);
        }
    }
}
