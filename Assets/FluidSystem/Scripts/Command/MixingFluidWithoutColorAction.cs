using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingFluidWithoutColorAction : IFluidAction
{
    private FluidContainer _container;
    private Fluid _fluidA;
    private Fluid _fluidB;
    private float _count;
    
    public MixingFluidWithoutColorAction(FluidContainer container, Fluid fluidA, Fluid fluidB, float count)
    {
        _container = container;
        _fluidA = fluidA;
        _fluidB = fluidB;
        _count = count;
    }
    
    public void Execute()
    {
        Fluid mixing = FluidHelper.Mixing(_fluidA.GetLastReactionMixing(), _fluidB, _count);
        Fluid fluid = _fluidA;
        //fluid.SetCount(mixing.GetCount());
        fluid.SetDensity(mixing.GetDensity());
        fluid.SetViscosity(mixing.GetViscosity());
        fluid.SetTimeToReactionMixing(mixing.GetTimeToReaction());
        fluid.SetIntensity(mixing.GetIntensity());
        fluid.SetSpeedMixing(mixing.GetSpeedMixing());
        fluid.SetIsDiffusion(mixing.IsDiffusion());
        fluid.SetTimeToDiffusion(mixing.GetTimeToDiffusion());
        fluid.SetSpeedDiffusion(mixing.GetSpeedDiffusion());
        fluid.SetFinalDiffusion(mixing.GetFinalDiffusion());
        
        if (!fluid.GetStatusReactionMixing())
        {
            fluid.SetColor(_fluidB.GetColor());
            fluid.StartReaction(mixing);
            _container.MixingWithTime(fluid);
        }
        else
        {
            fluid.StartReaction(mixing);
        }
    }
}
