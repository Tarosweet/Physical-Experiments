using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubstanceTransfusionTask : IShipSluiceTask
{
    private IShipSluiceTask _task;
    private CommunicatingVessels _vessels;
    private Ship _ship;
    private TransferHole _hole;
    private FluidContainer _container;

    public SubstanceTransfusionTask(CommunicatingVessels vessels, Ship ship, TransferHole hole, FluidContainer container)
    {
        _vessels = vessels;
        _container = container;
        _ship = ship;
        _hole = hole;
    }
    
    public void Start()
    {
        _vessels.isEqualAll += StartNext;
        _vessels.changedFluidLevel += MoveShip;
        _hole.Open();
        if(_vessels.IsEqualCheck())
            StartNext();
    }

    private void MoveShip(float y)
    {
        Vector3 pos = _ship.GetPosition();
        pos.y = _container.GetWaterLevel();
        
        _ship.SetPosition(pos);
    }

    public void Stop()
    {
        _vessels.isEqualAll -= StartNext;
        _vessels.changedFluidLevel -= MoveShip;
        _hole.Close();
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
