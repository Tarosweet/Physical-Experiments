using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppendFluidAction : IFluidAction
{
    private FluidContainer _container;
    private Fluid _fluid;

    public AppendFluidAction(FluidContainer container, Fluid fluid)
    {
        _container = container;
        _fluid = fluid;
    }
    
    public void Execute()
    {
        List<Fluid> fluids = _container.GetFluids();
        float density = _fluid.GetDensity();
        int i;
        
        for (i = 0; i < fluids.Count; i++)
        {
            if (fluids[i].GetDensity() < density)
            {
                break;
            }
        }

        fluids.Insert(i,_fluid);
        _container.SubscribeToFluid(_container.GetFluids()[i]);
        _container.OnChangeLiters();
        _container.OnChangeColor();
    }
}
