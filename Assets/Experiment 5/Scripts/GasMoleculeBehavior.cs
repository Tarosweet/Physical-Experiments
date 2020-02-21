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
                molecule.StopMove();
                //molecule.MoveTo(RandomDestination(), 2);
                molecule.StartMoveInRandomDirection(RandomDestination());
            }
        }

        private Vector3 RandomDestination()
        {
            return Random.insideUnitSphere * Random.Range(minDirectionVectorMultiplier,maxDirectionVectorMultiplier);
        }
    }
}
