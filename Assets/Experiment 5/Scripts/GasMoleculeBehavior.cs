using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Experiment_5.Scripts
{
    public class GasMoleculeBehavior : MoleculeBehavior
    {
        private Vector3 _currentDestination;
        
        protected override void MoveInNewDirection()
        {
            foreach (var molecule in moleculeGroup.molecules)
            {
                molecule.StartMoveInRandomDirection(RandomDestination());
            }
        }
    }
}
