using System;
using System.Security.Cryptography;
using Playground.SpringSystem;
using UnityEngine;

    public class Scales : MonoBehaviour
    {
        [SerializeField] private Hook _hook;
        private void OnEnable()
        {
            _hook.onHook += ShowMass;
        }

        private void OnDisable()
        {
            _hook.onHook -= ShowMass;
        }

        public float CalculateMass(IHavingMass mass)
        {
            return mass.GetMass();
        }

        private void ShowMass(JointsContainer container)
        {
            IHavingMass havingMass = container;

            if (container.IsChainExist())
                havingMass = container.weightsChain;
            
            Debug.Log(CalculateMass(havingMass));
        }
    }
