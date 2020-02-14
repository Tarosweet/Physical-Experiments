using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseFluidAction : IFluidAction
{
    private FluidContainer _container;
    private Fluid _fluid;
    private float _count;
    
    public IncreaseFluidAction(FluidContainer container, Fluid fluid, float count)
    {
        _container = container;
        _fluid = fluid;
        _count = Mathf.Abs(count);
    }
    
    public void Execute()
    {
        Fluid fluid = _fluid;
        fluid.SetCount(fluid.GetCount() + _count);
    }
}
