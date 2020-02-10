using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hook))]
public class UIInstallationSwitcher : SpringInstallationEvents
{

    [SerializeField] private FormulaUI _formulaUi;

    private void SetUI(JointsContainer container, bool value)
    {
        int index = 1;
        
        if (container.IsChainExist())
        {
            index = container.weightsChain.SetUI(value);
            _formulaUi.CalculateFormula(index); 
            return;
        }

        InstallationUI ui = container.GetComponent<InstallationUI>();
        if (ui)
        {
            _formulaUi.CalculateFormula(index);  //TODO refactor
            ui.Initialize(ref index);
            ui.SetActive(value);
        }

    }

    protected override void Connected(JointsContainer container)
    {
        SetUI(container,true);
        Hook = container.GetLastHookSequentially();
    }

    protected override void Disconnected(JointsContainer container)
    {
        SetUI(container,false);
        Hook = _startJointsContainer.GetLastHookSequentially(); 
        SetUI(Hook.jointsContainer, true);
    }
    
}
