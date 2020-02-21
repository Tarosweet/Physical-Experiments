using System;
using UnityEngine;

namespace Experiment_5.Scripts
{
    public class MoleculeGroup : MonoBehaviour
    {
        public MoleculeMovement[] molecules;
        
        public Vector3 boundBox = Vector3.one;
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(Vector3.zero, boundBox);
        }
    }
}
