using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransferHole
{
    [SerializeField] private FluidContainer _container;
    [Min(0)]
    [SerializeField] private float _speed;
    [SerializeField] private bool _isOpen;

    public bool IsOpen()
    {
        return _isOpen;
    }

    public void Close()
    {
        _isOpen = false;
    }

    public void Open()
    {
        _isOpen = true;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public FluidContainer GetContainer()
    {
        return _container;
    }
}
