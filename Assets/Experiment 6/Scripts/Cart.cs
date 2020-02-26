using System;
using UnityEngine;
using UnityEngine.Events;

namespace Experiment_6.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cart : MonoBehaviour
    {
        [SerializeField] private Vector3 startVelocityVector = Vector3.forward;
        [SerializeField] private float startForceMultiplier = 1;

        [SerializeField] private UnityEvent onStart;
        
        private Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnMouseDown()
        {
            Starter();
        }

        private void Starter()
        {
            rigidbody.velocity = startVelocityVector * startForceMultiplier;
            onStart?.Invoke();
        }
    }
}
