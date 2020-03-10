using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTask : IShipSluiceTask
{
    private Gate _gate;
    private IShipSluiceTask _task;
    
    public DoorOpenTask(Gate gate)
    {
        _gate = gate;
    }
    
    public void Start()
    {
        _gate.doorOpened += StartNext;
        _gate.OpenDoor();
    }

    public void Stop()
    {
        _gate.doorOpened -= StartNext;
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
