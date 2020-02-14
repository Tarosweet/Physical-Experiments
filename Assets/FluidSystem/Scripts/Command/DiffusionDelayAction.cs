using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffusionDelayAction : IFluidAction
{
    private FluidContainer _container;
    private Fluid _fluid;

    public DiffusionDelayAction(FluidContainer container, Fluid fluid)
    {
        _container = container;
        _fluid = fluid;
    }
    public void Execute()
    {
        if (!_fluid.GetStatusReactionDiffusion())
        {
            _container.DiffusionWithTime(_fluid);
        }
    }
}
