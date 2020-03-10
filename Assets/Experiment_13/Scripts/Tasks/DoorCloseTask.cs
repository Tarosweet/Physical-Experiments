using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseTask : IShipSluiceTask
{
    private Gate _gate;
    private IShipSluiceTask _task;
    
    public DoorCloseTask(Gate gate)
    {
        _gate = gate;
    }
    
    public void Start()
    {
        _gate.doorClosed += StartNext;
        _gate.CloseDoor();
    }

    public void Stop()
    {
        _gate.doorClosed -= StartNext;
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
