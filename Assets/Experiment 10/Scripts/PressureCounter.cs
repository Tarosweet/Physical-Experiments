using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Experiment_10.Scripts
{
    public class PressureCounter : MonoBehaviour
    {
        [SerializeField] private Transform handle;

        [SerializeField] private float minPressure = 2;
        [SerializeField] private float maxPressure = 10;

        [SerializeField] private Text text;

        [SerializeField] private LockPosition lockedVector;

        private LockPosition.LockedVariable ClampedVariable => lockedVector.lockedPosition.y;

        private void Update()
        { 
            text.text = CalculatePressure().ToString("F") + Units.Pressure.Pascal;
        }

        private float CalculatePressure()
        {
            var value = Mathf.InverseLerp(ClampedVariable.max, ClampedVariable.min, handle.position.y);
            return Mathf.Lerp(minPressure, maxPressure, value);
        }
    }
}
