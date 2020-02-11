using UnityEngine;

namespace Experiment_4_new.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Attractable : MonoBehaviour, IAttractable
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }
    }
}