
using System.Collections.Generic;
using UnityEngine;

public class FluidActionContainer : IFluidActionContainer, IFluidAction
{
    private List<IFluidAction> _actions;

    public FluidActionContainer()
    {
        _actions = new List<IFluidAction>();
    }

    public void Add(IFluidAction action)
    {
        _actions.Add(action);
    }

    public void Remove(IFluidAction action)
    {
        _actions.Remove(action);
    }

    public void Execute()
    {
        foreach (var action in _actions)
        {
            action.Execute();
        }
    }
}
