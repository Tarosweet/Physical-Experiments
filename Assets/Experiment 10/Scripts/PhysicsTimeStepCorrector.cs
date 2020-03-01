using UnityEngine;

namespace Experiment_10.Scripts
{
    public class PhysicsTimeStepCorrector : MonoBehaviour
    {
        [SerializeField] private float newTimeStep = 0.008f;
        private void Start()
        {
            Time.fixedDeltaTime = newTimeStep;
        }
    
    }
}
