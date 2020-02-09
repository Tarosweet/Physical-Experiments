using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hook))]
public class UIInstallationSwitcher : MonoBehaviour
{
    [SerializeField] private Hook[] _hooks;
    
    [SerializeField] private Hook _hook;
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

     private void OnEnable()
    {
        Listen();

        foreach (var hook in _hooks)
        {
            hook.onDisconnectHook += Disconnected; //TODO refactor
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

    private void Listen()
    {
        _hook.onHook += Connected;
    }

    private void StopListen()
    {
        _hook.onHook -= Connected;
    }

    private void SetUI(JointsContainer container, bool value)
    {
        if (container.IsChainExist())
        {
            container.weightsChain.SetUI(value);
            return;
        }
        
        container.GetComponent<InstallationUI>().SetActive(value);
    }

    private void Connected(JointsContainer container)
    {
        SetUI(container,true);
        Hook = container.GetLastHookSequentially();
    }

    private void Disconnected(JointsContainer container)
    {
        SetUI(container,false);
        Hook = GetComponent<JointsContainer>().GetLastHookSequentially();
    }
    
}
