using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicatingVesselsBuilder : IFluidActionBuilder
{
    private FluidContainer _fromContainer;
    private List<TransferHole> _holes;

    public CommunicatingVesselsBuilder(FluidContainer fromContainer, List<TransferHole> holes)
    {
        _fromContainer = fromContainer;
        _holes = holes;
    }
    public IFluidAction Build()
    {
        float rate = CalculateAverageRate(GetTransferContainers());
        return Transfer(_holes, rate);
    }

    private List<FluidContainer> GetTransferContainers()
    {
        List<FluidContainer> containers = new List<FluidContainer>();
        
        foreach (var hole in _holes)
        {
            FluidContainer container = hole.GetContainer();

            if (_fromContainer.GetWaterLevel() > container.GetWaterLevel() && !container.IsFull() && hole.IsOpen())
            {
                containers.Add(container);
            }
        }
        
        return containers;
    }

    private float CalculateAverageRate(List<FluidContainer> containers)
    {
        float liters = _fromContainer.GetLitersFluid();
        float rates = _fromContainer.GetWaterRate();
        
        foreach (var container in containers)
        {
            liters += container.GetLitersFluid();
            rates += container.GetWaterRate();
        }

        return liters / rates;
    }

    private IFluidAction Transfer(List<TransferHole> holes, float rate)
    {
        List<Fluid> fluids = _fromContainer.GetFluids();
        
        IFluidActionContainer containerActions = new FluidActionContainer();
        
        foreach (var hole in holes)
        {
            if(!hole.IsOpen()) continue;
            
            float count = hole.GetSpeed();
            FluidContainer container = hole.GetContainer();
            float level = container.GetWaterRate() * rate - container.GetLitersFluid();
            
            if (level < count)
            {
                count = level;
            }

            while (count > 0)
            {
                Fluid to = FluidHelper.GetFluidWithTheSameDensity(container.GetFluids(), fluids[0]);
                float liters = count;
                
                if (to == null)
                {
                    Fluid newFluid = fluids[0].Clone();

                    if (liters > fluids[0].GetCount())
                    {
                        liters = fluids[0].GetCount();
                        fluids.RemoveAt(0);
                    }
                    newFluid.SetCount(liters);
                    
                    containerActions.Add(new AppendFluidAction(container, newFluid));
                }
                else
                {
                    if (liters > fluids[0].GetCount())
                    {
                        liters = fluids[0].GetCount();
                        fluids.RemoveAt(0);
                    }
                    
                    containerActions.Add(new IncreaseFluidAction(container, to, liters));
                }
                
                containerActions.Add(new DecreaseFluidAction(_fromContainer, fluids[0], liters));
                
                count -= liters;
            }
        }

        return (IFluidAction)containerActions;
    }
    
    
}
