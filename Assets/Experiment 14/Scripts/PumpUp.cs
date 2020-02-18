namespace Experiment_14.Scripts
{
    public class PumpUp : PumpBehavior
    {
        private float pumpCount = 500f;

        public override void Pump(IPumped pumped)
        {
            pumped.Pumped(pumpCount);
        }
    }
}