using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatewayOpeningTask : IShipSluiceTask
{
    private Gate _gate;
    private IShipSluiceTask _task;
    
    public GatewayOpeningTask(Gate gate)
    {
        _gate = gate;
    }
    
    public void Stop()
    {
        _gate.gateOpened -= StartNext;
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
        _gate.gateOpened += StartNext;
        _gate.OpenGate();
    }
    
}
