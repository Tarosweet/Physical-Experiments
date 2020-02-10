using System;
using System.Security.Cryptography;
using Playground.SpringSystem;
using UnityEngine;
using UnityEngine.UI;

public class Scales : SpringInstallationEvents
{
        [SerializeField] private ScalesUI ui;

        private float CalculateMass(IHavingMass mass)
        {
            return mass.GetMass();
        }

        private void ShowMass(JointsContainer container)
        {
            IHavingMass havingMass = container;

            if (container.IsChainExist())
                havingMass = container.weightsChain;
            
            ui.EnableUI(CalculateMass(havingMass));
        }
        
        protected override void Connected(JointsContainer container)
        {
            ShowMass(container);
            Hook = container.GetLastHookSequentially();
        }

        protected override void Disconnected(JointsContainer container)
        {
            ui.DisableUI();
            Hook = _startJointsContainer.GetLastHookSequentially();
            ui.EnableUI(Hook.jointsContainer.GetMass());
        }
    }
