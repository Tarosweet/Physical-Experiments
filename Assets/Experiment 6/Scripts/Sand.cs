using UnityEngine;

namespace Experiment_6.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Sand : MonoBehaviour
    {
        public float strength = 0.05f;
    
        private void OnTriggerStay(Collider other)
        {
            var rigidbodyInSand = other.GetComponent<Rigidbody>();
        
            if (!rigidbodyInSand)
                return;
        
            StopRigidbody(rigidbodyInSand);
        }

        private void StopRigidbody(Rigidbody rigidbody)
        {
            rigidbody.velocity = rigidbody.velocity * strength;
        }
    }
}
