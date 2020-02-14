using UnityEngine;

namespace Experiment_14.Scripts
{
    public class PumpDown : PumpBehavior
    {
        private float pumpCount = 20f;

        public override void Pump(IPumped pumped)
        {
            pumped.Pumped(pumpCount);
        }
    }
}