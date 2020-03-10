using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixingPressTask : ITask
{
    private QuestionObserverSolids _observer;
    private Press _press;
    private bool _isEnd;
    
    public FixingPressTask(QuestionObserverSolids observer, Press press)
    {
        _observer = observer;
        _press = press;
        _isEnd = false;
        _press.OnToEndPositionOfAnimation += EndAnimationFixingPress;
        _observer.GetGoldNode().OnDisonnectNode += _observer.RefreshQuestion;
        _observer.GetPlumbumNode().OnDisonnectNode += _observer.RefreshQuestion;
    }

    private void EndAnimationFixingPress()
    {
        _isEnd = true;
        Remove();
        ITask task = new ChangeSolidTask(_observer,_observer.GetSolidChanger());
        _observer.ChangeTask(task);
        _observer._fixedPress = true;
    }
    
    public void Start()
    {
        
    }

    public void Remove()
    {
        _press.OnToEndPositionOfAnimation -= EndAnimationFixingPress;
        _observer.GetGoldNode().OnDisonnectNode -= _observer.RefreshQuestion;
        _observer.GetPlumbumNode().OnDisonnectNode -= _observer.RefreshQuestion;
    }
}
