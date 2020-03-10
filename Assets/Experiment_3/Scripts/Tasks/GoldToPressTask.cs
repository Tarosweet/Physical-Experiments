using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldToPressTask : ITask
{
    private QuestionObserverSolids _observer;
    private Node _gold;
    private Node _press;
    private bool _isEnd;

    public GoldToPressTask(QuestionObserverSolids observer, Node gold, Node press)
    {
        _observer = observer;
        _gold = gold;
        _press = press;
        _isEnd = false;
        _gold.OnConnectNode += Connect;
        _gold.OnDisonnectNode += _observer.RefreshQuestion;
    }

    public void Start()
    {
        
    }

    public void Remove()
    {
        _gold.OnConnectNode -= Connect;
        _gold.OnDisonnectNode -= _observer.RefreshQuestion;
    }

    private void Connect(Node node)
    {
        if (node == _press)
        {
            _isEnd = true;
            Remove();
            ITask task = new PlumbumToGoldTask(_observer,_observer.GetPlumbumNode(),_gold);
            _observer.ChangeTask(task);
            _observer._goldToPress = true;
        }
    }
}
