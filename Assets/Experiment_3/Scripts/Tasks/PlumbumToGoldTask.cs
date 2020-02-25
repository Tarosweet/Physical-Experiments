using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumbumToGoldTask : ITask
{
    private QuestionObserverSolids _observer;
    private Node _plumbum;
    private Node _gold;
    private bool _isEnd;

    public PlumbumToGoldTask(QuestionObserverSolids observer, Node plumbum, Node gold)
    {
        _observer = observer;
        _plumbum = plumbum;
        _gold = gold;
        _isEnd = false;
        _plumbum.OnConnectNode += Connect;
        _observer.GetGoldNode().OnDisonnectNode += _observer.RefreshQuestion;
    }

    public void Start()
    {
        
    }

    public void Remove()
    {
        _plumbum.OnConnectNode -= Connect;
        _observer.GetGoldNode().OnDisonnectNode -= _observer.RefreshQuestion;
    }

    private void Connect(Node node)
    {
        if (node == _gold)
        {
            _isEnd = true;
            Remove();
            ITask task = new FixingPressTask(_observer,_observer.GetPress());
            _observer.ChangeTask(task);
            _observer._plumbubToPress = true;
        }
    }
}
