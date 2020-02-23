namespace Experiment_5.Scripts
{
    public class LiquidMoleculeBehavior : MoleculeBehavior
    {
        protected override void MoveInNewDirection()
        {
            var position = RandomDestination();
            foreach (var molecule in moleculeGroup.molecules)
            { 
                var offset = molecule.moleculeTransform.position;
                molecule.StopMove();
                molecule.MoveTo(position + offset, RandomTimeToReachDestinationPoint());
            }
        }
    }
}
