using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTask : ITask
{
    private QuestionObserverSolids _observer;
    
    public FinishTask(QuestionObserverSolids observer)
    {
        _observer = observer;
    }
    public void Start()
    {
        _observer._isFinish = true;
    }

    public void Remove()
    {
        
    }
}
