using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShipTask : IShipSluiceTask
{
    private Ship _ship;
    private FluidContainer _container;
    private IShipSluiceTask _task;
    
    public MoveShipTask(Ship ship, FluidContainer container)
    {
        _ship = ship;
        _container = container;
    }
    public void Start()
    {
        _ship.moveEnd += StartNext;

        Vector3 toPosition = _container.GetPosition();
        toPosition.y = _container.GetWaterLevel();
        
        _ship.MoveTo(toPosition);
    }

    public void Stop()
    {
        _ship.moveEnd -= StartNext;
    }

    public void SetNext(IShipSluiceTask task)
    {
        _task = task;
    }

    public void StartNext()
    {
        Stop();
        _task?.Start();
    }
}
