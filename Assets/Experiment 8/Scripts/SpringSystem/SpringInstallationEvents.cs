
    using System;
    using UnityEngine;

    public abstract class SpringInstallationEvents : MonoBehaviour
    {
        [SerializeField] private Hook _hook;
        
        protected Hook Hook
        {
            set
            {
                StopListen();
                _hook = value;
                Listen();
            }
            get => _hook;
        }
        
        [SerializeField] private Hook[] _hooks;
        
        protected JointsContainer _startJointsContainer;
        
        private void Awake()
        {
            _hooks = FindObjectsOfType<Hook>();
            _startJointsContainer = GetComponent<JointsContainer>();
        }
        

        private void Listen()
        {
            _hook.onHook += Connected;
        }

        private void StopListen()
        {
            _hook.onHook -= Disconnected;
        }
        
        private void OnEnable()
        {
            Listen();

            foreach (var hook in _hooks)
            {
                hook.onDisconnectHook += Disconnected; 
            }
        }

        private void OnDisable()
        {
            StopListen();
        
            foreach (var hook in _hooks)
            {
                hook.onDisconnectHook -= Disconnected;
            }
        }

        protected abstract void Connected(JointsContainer container);

        protected abstract void Disconnected(JointsContainer container);

    }
