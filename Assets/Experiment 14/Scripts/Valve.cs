using System;
using UnityEngine;

namespace Experiment_14.Scripts
{
    public class Valve : MonoBehaviour
    {
        [SerializeField] private AtmospherePressure atmospherePressure;

        private void OnMouseDown()
        {
            LetTheAir();
            Debug.Log("Clicked");
        }

        private void LetTheAir()
        {
            atmospherePressure.Unpress();
        }
    }
}
