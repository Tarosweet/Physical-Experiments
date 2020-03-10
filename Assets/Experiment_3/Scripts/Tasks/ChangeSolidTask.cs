using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSolidTask : ITask
{
    private QuestionObserverSolids _observer;
    private SolidChanger _solidChanger;
    bool _isEnd;
    public ChangeSolidTask(QuestionObserverSolids observer, SolidChanger changer)
    {
        _isEnd = false;
        _observer = observer;
        _solidChanger = changer;
    }
    public void Start()
    {
        SolidDiffusion solidDiffusion = _solidChanger.Change(_observer.GetGoldNode(), _observer.GetPlumbumNode(),
            _observer.GetPressNode(),_observer.GetNodeContainer());
        _observer.SetSolidDiffusion(solidDiffusion);
        ITask task = new SolidDiffusionTask(_observer, solidDiffusion);
        _isEnd = true;
        _observer.ChangeTask(task);
        _observer._changedSolids = true;
    }

    public void Remove()
    {
        
    }
}
