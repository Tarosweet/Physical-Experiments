using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionObserverSolids : MonoBehaviour
{
    [SerializeField] private Node _goldNode;
    [SerializeField] private Node _plumbumNode;
    [SerializeField] private Node _pressNode;
    [SerializeField] private Press _press;
    [SerializeField] private SolidChanger _solidChanger;
    [SerializeField] private SolidDiffusion _solidDiffusion;
    [SerializeField] private NodeContainer _nodeContainer;
    [SerializeField] private ITask _task;
    
    [SerializeField] public bool _goldToPress;
    [SerializeField] public bool _plumbubToPress;
    [SerializeField] public bool _fixedPress;
    [SerializeField] public bool _changedSolids;
    [SerializeField] public bool _diffusionIsEnd;
    [SerializeField] public bool _isFinish;

    public void Start()
    {
        _task = new GoldToPressTask(this, _goldNode, _pressNode);
    }

    public void RefreshQuestion()
    {
        _task.Remove();
        _task = new GoldToPressTask(this,_goldNode,_pressNode);
        _goldToPress = false;
        _plumbubToPress = false;
        _fixedPress = false;
        _changedSolids = false;
        _diffusionIsEnd = false;
        _isFinish = false;
    }
    public void ChangeTask(ITask task)
    {
        _task = task;
        task?.Start();
    }

    public Node GetGoldNode()
    {
        return _goldNode;
    }

    public Node GetPlumbumNode()
    {
        return _plumbumNode;
    }

    public Node GetPressNode()
    {
        return _pressNode;
    }

    public Press GetPress()
    {
        return _press;
    }

    public SolidChanger GetSolidChanger()
    {
        return _solidChanger;
    }

    public SolidDiffusion GetSolidDiffusion()
    {
        return _solidDiffusion;
    }

    public void SetSolidDiffusion(SolidDiffusion solidDiffusion)
    {
        _solidDiffusion = solidDiffusion;
    }

    public NodeContainer GetNodeContainer()
    {
        return _nodeContainer;
    }
}
