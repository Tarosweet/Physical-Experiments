using System.Collections.Generic;
using UnityEngine;

namespace Experiment_5.Scripts
{
    public class SolidMoleculeBehavior : MoleculeBehavior
    {
        private class MoleculePoints
        {
            public Vector3 LeftPoint;
            public Vector3 RightPoint;
        }
        
        private int _turn;
        private List<MoleculePoints> _moleculePoints = new List<MoleculePoints>();

        protected new void Start()
        {
            _moleculePoints = InitializeMoleculePoints();
            
            base.Start();
        }
        protected override void MoveInNewDirection()
        {
            for (var index = 0; index < moleculeGroup.molecules.Length; index++)
            {
                var molecule = moleculeGroup.molecules[index];
                molecule.StopMove();
                
                molecule.MoveTo(IsEven(_turn) ? _moleculePoints[index].LeftPoint : _moleculePoints[index].RightPoint,
                    RandomTimeToReachDestinationPoint());

                _turn++;
            }
            _turn++;
        }
        

        private static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        private List<MoleculePoints> InitializeMoleculePoints()
        {
            var moleculePoints = new List<MoleculePoints>();
            
            foreach (var molecule in moleculeGroup.molecules)
            {
                var position = molecule.moleculeTransform.position;
                var right = molecule.moleculeTransform.right;
                
                var moleculePoint = new MoleculePoints
                {
                    LeftPoint = position + RandomDirectionVectorMultiplier() * -1 * right,
                    RightPoint = position + right * RandomDirectionVectorMultiplier()
                };
                
                moleculePoints.Add(moleculePoint);
            }

            return moleculePoints;
        }
    }
}
