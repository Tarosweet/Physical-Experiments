using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public static class FluidHelper
{
    public static Fluid Mixing(Fluid fluidA, Fluid fluidB, float countFluidB)
    {
        Fluid f = new Fluid(Color.white, 0);
        float countA = fluidA.GetCount();
        float ratio = countFluidB / (countA + countFluidB);
        
        f.SetCount(fluidA.GetCount());
        f.IncreaseCount(countFluidB);
        
        Color newColor = Color.Lerp(fluidA.GetColor(), fluidB.GetColor(),
            Mathf.Clamp(ratio * fluidB.GetIntensity()/fluidA.GetIntensity(), 0, 1));
        f.SetColor(newColor);
        
        f.SetDensity(fluidA.GetDensity());
        
        float newViscosity = Mathf.Lerp(fluidA.GetViscosity(), fluidB.GetViscosity(),
            Mathf.Clamp(ratio, 0, 1));
        f.SetViscosity(newViscosity);
        
        float newIntensity = Mathf.Lerp(fluidA.GetIntensity(), fluidB.GetIntensity(),
            Mathf.Clamp(ratio, 0, 1));
        f.SetIntensity(newIntensity);

        float newTimeToReaction = fluidA.GetTimeToReaction() > fluidB.GetTimeToReaction()
            ? fluidA.GetTimeToReaction()
            : fluidB.GetTimeToReaction();
        f.SetTimeToReaction(newTimeToReaction);

        float newSpeedMixing = fluidB.GetSpeedMixing(); //Mathf.Lerp(fluidA.GetSpeedMixing(), fluidB.GetSpeedMixing(),Mathf.Clamp(ratio, 0, 1));
        f.SetSpeedMixing(newSpeedMixing);
        
        return f;
    }

    [CanBeNull]
    public static Fluid GetFluidWithTheSameDensity(List<Fluid> _fluids, Fluid from)
    {
        foreach (var fluid in _fluids)
        {
            if (Math.Abs(@from.GetDensity() - fluid.GetDensity()) < 0.00001)
                return fluid;
        }

        return null;
    }

}
