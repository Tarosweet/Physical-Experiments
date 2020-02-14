using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpTransferFluidActionBuilder : IFluidActionBuilder
{
    private FluidContainer _containerFrom;
    private FluidContainer _containerTo;
    private float _count;
    
    public UpTransferFluidActionBuilder(FluidContainer containerFrom, FluidContainer containerTo, float count)
    {
        _containerFrom = containerFrom;
        _containerTo = containerTo;
        _count = Mathf.Abs(count);
    }

    public IFluidAction Build()
    {
        IFluidActionContainer containerActions = new FluidActionContainer();
        IFluidAction action;
        List<Fluid> fluidsFrom = _containerFrom.GetFluids();

        List<Fluid> useFluids = new List<Fluid>();
        List<float> counts = new List<float>();

        float usesCount = _count;
        for (int i = fluidsFrom.Count - 1; i >= 0; i--)
        {
            float countInFluid = fluidsFrom[i].GetCount();
            useFluids.Add(fluidsFrom[i]);
            if (countInFluid < usesCount)
            {
                counts.Add(countInFluid);
                usesCount -= countInFluid;
            }
            else
            {
                counts.Add(usesCount);
                break;
            }
        }

        if (_containerTo != null)
        {
            containerActions.Add(Transfer(_containerTo, useFluids, counts));
        }

        for (int i = 0; i < useFluids.Count; i++)
        {
            action = new DecreaseFluidAction(_containerFrom, useFluids[i], counts[i]);
            containerActions.Add(action);
        }

        return (IFluidAction)containerActions;
    }

    private IFluidAction Transfer(FluidContainer container, List<Fluid> fluids, List<float> counts)
    {
        IFluidActionContainer actionContainer = new FluidActionContainer();
        IFluidAction action = null;
        List<Fluid> fluidsTo = _containerTo.GetFluids();
        float diff = container.GetLitersFluid() + _count - container.GetMaxLiters();
        
        if (diff >= 0)
        {
            List<Fluid> diffFluids = container.GetFluids();
            float currDiff = diff;
            int i = diffFluids.Count - 1;
            while (currDiff > 0)
            {
                Fluid tmpFluid = diffFluids[i];
                float count = diffFluids[i].GetCount();
                if (count > currDiff)
                {
                    action = new DecreaseFluidAction(container, tmpFluid, currDiff);
                    currDiff = 0;
                }
                else
                {
                    action = new DecreaseFluidAction(container,tmpFluid,tmpFluid.GetCount());
                    currDiff -= tmpFluid.GetCount();
                    i--;
                }
                actionContainer.Add(action);
            }
        }
        
        for (int i = 0; i < fluids.Count; i++)
        {
            Fluid to = FluidHelper.GetFluidWithTheSameDensity(fluidsTo, fluids[i]);
            Fluid newFluid = fluids[i].Clone();
            newFluid.SetCount(counts[i]);
            if (to != null)
            {
                actionContainer.Add(new IncreaseFluidAction(_containerTo, to, counts[i]));

                if (!newFluid.IsTimeMixing())
                {
                    if (to.IsTimeMixing())
                    {
                        actionContainer.Add(new MixingFluidWithoutColorAction(_containerTo,to,newFluid,counts[i]));
                    }
                    else
                    {
                        actionContainer.Add(new MixingFluidAction(_containerTo, to, newFluid, counts[i]));
                    }
                }
                else
                {
                    actionContainer.Add(new MixingFluidWithTimeAction(_containerTo, to, newFluid, counts[i]));
                }

                if (newFluid.IsDiffusion() || to.IsDiffusion())
                {
                    actionContainer.Add(new DiffusionDelayAction(_containerTo, to));
                }
                
            }
            else
            {
                actionContainer.Add(new AppendFluidAction(_containerTo, newFluid));
            }
        }

        return (IFluidAction)actionContainer;
    }
}
