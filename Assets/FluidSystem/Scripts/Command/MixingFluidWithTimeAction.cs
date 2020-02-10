using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingFluidWithTimeAction : IFluidAction
{
    private FluidContainer _container;
    private Fluid _fluidA;
    private Fluid _fluidB;
    private float _count;
    
    public MixingFluidWithTimeAction(FluidContainer container, Fluid fluidA, Fluid fluidB, float count)
    {
        _container = container;
        _fluidA = fluidA;
        _fluidB = fluidB;
        _count = count;   
    }
    
    public void Execute()
    {

        Fluid fluid = _fluidA;
        
        Fluid newFluid = FluidHelper.Mixing(fluid.GetLastReaction(), _fluidB, _count);
        fluid.SetCount(fluid.GetCount()+_count);
        fluid.SetDensity(newFluid.GetDensity());
        fluid.SetViscosity(newFluid.GetViscosity());
        fluid.SetTimeToReaction(newFluid.GetTimeToReaction());
        fluid.SetIntensity(newFluid.GetIntensity());
        fluid.SetSpeedMixing(newFluid.GetSpeedMixing());
        
        if (!fluid.GetStatusReaction())
        {
            fluid.StartReaction(newFluid);
            _container.MixingWithTime(fluid);
        }
        else
        {
            fluid.StartReaction(newFluid);
        }
    }
}
