using System;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Experiment_14.Scripts
{
    public class DisplayingMassText : MonoBehaviour
    {
        private enum Unit
        {
            Newton,
            Kilogram
        }
    
        [SerializeField] private Text text;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private Unit unit;

        private void Start()
        {
            SetText();
        }

        private void OnValidate()
        {
            SetText();
        }

        private void SetText()
        {
            var mass = _rigidbody.mass;

            if (unit == Unit.Newton)
            {
                mass = RigidbodyExtension.ConvertToNewton(mass);
            }

            text.text = mass.ToString();
        }
    }
}
