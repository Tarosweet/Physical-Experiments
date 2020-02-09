using System;
using System.Security.Cryptography;
using Playground.SpringSystem;
using UnityEngine;
using UnityEngine.UI;

public class Scales : MonoBehaviour
    {
        [SerializeField] private Hook _hook;
        [SerializeField] private ScalesUI ui;
        
        [SerializeField] private Hook[] _hooks;
        
        private Hook Hook
        {
            set
            {
                StopListen();
                _hook = value;
                Listen();
            }
            get => _hook;
        }
        
        private void Awake()
        {
            _hooks = FindObjectsOfType<Hook>();
        }
        
        private void Listen()
        {
            _hook.onHook += Connected;
        }

        private void StopListen()
        {
            _hook.onHook -= Connected;
        }
        
        private void OnEnable()
        {
            _hook.onHook += ShowMass;
            
            foreach (var hook in _hooks)
            {
                hook.onDisconnectHook += Disconnected; //TODO refactor
            }
        }

        private void OnDisable()
        {
            _hook.onHook -= ShowMass;
            
            foreach (var hook in _hooks)
            {
                hook.onDisconnectHook -= Disconnected; //TODO refactor
            }
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
            
            ui.EnableUI(CalculateMass(havingMass));
        }
        
        private void Connected(JointsContainer container)
        {
            ShowMass(container);
            Hook = container.GetLastHookSequentially();
        }

        private void Disconnected(JointsContainer container)
        {
            ui.DisableUI();
            Hook = GetComponent<JointsContainer>().GetLastHookSequentially(); //TODO refactor
            ui.EnableUI(Hook.jointsContainer.GetMass());
        }
    }
