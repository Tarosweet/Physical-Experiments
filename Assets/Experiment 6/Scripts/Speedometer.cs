using Extensions;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Experiment_6.Scripts
{
    public class Speedometer : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Text text;

        [SerializeField] private VectorExtension.Axis axis = VectorExtension.Axis.X;
        
        private void Update()
        {
            text.text = "V = " + VectorExtension.GetAxisValueFromVector(rigidbody.velocity, axis).ToString("F1") + Units.Speed.MetersPerSeconds;
        }
    }
}
