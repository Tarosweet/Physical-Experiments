using System;
using UnityEngine;

namespace Helpers
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private float maxDistance = Mathf.Infinity;
        [SerializeField] private Vector3 direction;

        [SerializeField] private bool debug;
        
        private Transform _transform;
    
        private void Start()
        {
            _transform = transform;
        }

        public bool ThrowRaycast(out RaycastHit hit)
        {
            if (Physics.Raycast(_transform.position, _transform.TransformDirection(direction), out hit, maxDistance))
            {
                if (debug)
                    Debug.DrawRay(_transform.position, _transform.TransformDirection(direction) * hit.distance, Color.yellow);
                
                return true;
            }
            else
            {
                if (debug)
                    Debug.DrawRay(_transform.position, _transform.TransformDirection(direction) * 1000, Color.white);
                
                return false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!debug)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.TransformDirection(direction) * 1000 );
        }
    }
}
