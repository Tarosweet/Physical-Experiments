namespace Experiment_14.Scripts
{
    public class PumpUp : PumpBehavior
    {
        private float pumpCount = 400f;

        public override void Pump(IPumped pumped)
        {
            pumped.Pumped(pumpCount);
        }
    }
}