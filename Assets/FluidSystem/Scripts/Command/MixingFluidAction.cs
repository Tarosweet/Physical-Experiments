using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingFluidAction : IFluidAction
{
    private FluidContainer _container;
    private Fluid _fluidA;
    private Fluid _fluidB;
    private float _count;
    
    public MixingFluidAction(FluidContainer container, Fluid fluidA, Fluid fluidB, float count)
    {
        _container = container;
        _fluidA = fluidA;
        _fluidB = fluidB;
        _count = count;
    }
    
    public void Execute()
    {
        Fluid mixing = FluidHelper.Mixing(_fluidA, _fluidB, _count);
        Fluid fluid = _fluidA;
        fluid.SetColor(mixing.GetColor());
        //fluid.SetCount(mixing.GetCount());
        fluid.SetDensity(mixing.GetDensity());
        fluid.SetViscosity(mixing.GetViscosity());
        fluid.SetIsTimeMixing(mixing.IsTimeMixing());
        fluid.SetTimeToReactionMixing(mixing.GetTimeToReaction());
        fluid.SetIntensity(mixing.GetIntensity());
        fluid.SetSpeedMixing(mixing.GetSpeedMixing());
        fluid.SetIsDiffusion(mixing.IsDiffusion());
        fluid.SetTimeToDiffusion(mixing.GetTimeToDiffusion());
        fluid.SetSpeedDiffusion(mixing.GetSpeedDiffusion());
        fluid.SetFinalDiffusion(mixing.GetFinalDiffusion());
        //fluid.SetMergeAfterDiffusion(mixing.IsMergeAfterDiffusion());
    }
}
