using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicatingVessels : MonoBehaviour
{
    [SerializeField] private FluidContainer _thisContainer;
    [SerializeField] private List<TransferHole> _holes;
    
    private IFluidActionBuilder _actionBuilder;

    private void Update()
    {
        if (IsOpen())
        {
            Transfer();
        }
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
        _actionBuilder = new CommunicatingVesselsBuilder(_thisContainer,_holes);
        _actionBuilder.Build().Execute();
    }
}
