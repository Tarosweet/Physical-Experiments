using System;
using UnityEngine;

namespace Experiment_5.Scripts
{
    public class MoleculeGroup : MonoBehaviour
    {
        public MoleculeMovement[] molecules;

        [SerializeField] private Vector3 boundCenter = Vector3.zero;
        [SerializeField] private Vector3 boundSize = Vector3.one;

        private Bounds _bounds;

        public bool IsOutOfBounds(Vector3 point)
        {
            return !_bounds.Contains(point);
        }
        
        
        private void Start()
        {
            ConstructBounds(boundCenter, boundSize);
        }

        private void OnValidate()
        {
            ConstructBounds(boundCenter, boundSize);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(boundCenter, boundSize);
        }

        private void ConstructBounds(Vector3 center, Vector3 size)
        {
            _bounds = new Bounds(center, size);
        }
    }
}
