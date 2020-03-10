using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatewayCloseTask : IShipSluiceTask
{
    private Gate _gate;
    private IShipSluiceTask _task;
    
    public GatewayCloseTask(Gate gate)
    {
        _gate = gate;
    }
    
    public void Stop()
    {
        _gate.gateClosed -= StartNext;
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

    public void Start()
    {
        _gate.gateClosed += StartNext;
        _gate.CloseGate();
    }
}
