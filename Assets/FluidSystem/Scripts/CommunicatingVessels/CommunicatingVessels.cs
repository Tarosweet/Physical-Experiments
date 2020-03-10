using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicatingVessels : MonoBehaviour
{
    [SerializeField] private FluidContainer _thisContainer;
    [SerializeField] private List<TransferHole> _holes;
    
    private IFluidActionBuilder _actionBuilder;

    public Action isEqualAll;
    public Action<float> changedFluidLevel;
    
    private void Update()
    {
        if (IsOpen())
        {
            Transfer();
        }
    }

    public FluidContainer GetContainer()
    {
        return _thisContainer;
    }

    public List<TransferHole> GetHoles()
    {
        return _holes;
    }
    
    private bool IsOpen()
    {
        foreach (var hole in _holes)
        {
            if (hole.IsOpen())
                return true;
        }

        return false;
    }

    private void Transfer()
    {
        _actionBuilder = new CommunicatingVesselsBuilder(_thisContainer, _holes);
        _actionBuilder.Build().Execute();


        changedFluidLevel?.Invoke(_thisContainer.GetWaterLevel());

        if (IsEqualCheck())
            isEqualAll?.Invoke();
    }

    public bool IsEqualCheck()
    {
        foreach (var hole in _holes)
        {
            if (Math.Abs(hole.GetContainer().GetLitersFluid() - _thisContainer.GetLitersFluid()) > 1 && hole.IsOpen())
                return false;
        }

        return true;
    }
}
