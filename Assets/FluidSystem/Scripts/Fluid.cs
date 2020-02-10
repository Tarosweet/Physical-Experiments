using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Fluid
{
    [SerializeField] private Color _color;
    [SerializeField] private float _count;
    [SerializeField] private float _intensity;
    [SerializeField] private float _time;
    [SerializeField] private float _density;
    [SerializeField] private float _viscosity;
    [SerializeField] private float _speedMixing;
    [SerializeField] private bool _isReaction = false;
    [SerializeField] private Queue<Fluid> _toFluidReaction;
    public Action onChangeColor;
    public Action onChangeCount;
    public Action<Fluid> onZeroCount;

    public Fluid(Color color, float density)
    {
        _color = color;
        _count = 0;
        _intensity = 1;
        _time = 0;
        _density = density;
        _viscosity = 1;
        _toFluidReaction = new Queue<Fluid>();
    }

    public Color GetColor()
    {
        return _color;
    }

    public void SetColor(Color color)
    {
        _color = color;

        if (onChangeColor != null) 
            onChangeColor.Invoke();
    }

    public float GetCount()
    {
        return _count;
    }

    public void SetCount(float count)
    {
        _count = count;
        
        if (onChangeCount != null) 
            onChangeCount.Invoke();
        
        if (_count <= 0)
        {
            if(onZeroCount != null)
                onZeroCount.Invoke(this);
        }
    }

    public void IncreaseCount(float count)
    {
        count = Mathf.Abs(count);
        _count += count;
        
        if (onChangeCount != null) 
            onChangeCount.Invoke();
    }

    public float GetIntensity()
    {
        return _intensity;
    }

    public void SetIntensity(float intensity)
    {
        _intensity = intensity;
    }

    public float GetTimeToReaction()
    {
        return _time;
    }

    public void SetTimeToReaction(float time)
    {
        _time = time;
    }

    public float GetDensity()
    {
        return _density;
    }

    public void SetDensity(float density)
    {
        _density = density;
    }

    public float GetViscosity()
    {
        return _viscosity;
    }

    public void SetViscosity(float viscosity)
    {
        _viscosity = viscosity;
    }

    public float GetSpeedMixing()
    {
        return _speedMixing;
    }

    public void SetSpeedMixing(float speed)
    {
        _speedMixing = speed;
    }

    public void StartReaction(Fluid fluid)
    {
        if(_toFluidReaction == null)
            _toFluidReaction = new Queue<Fluid>();
       
        if (_toFluidReaction.Count >= 0)
            _isReaction = true;

        _toFluidReaction.Enqueue(fluid);
    }

    public bool GetStatusReaction()
    {
        if(_toFluidReaction == null)
            _toFluidReaction = new Queue<Fluid>();

        return _toFluidReaction.Count > 0;
    }

    public Fluid GetReaction()
    {
        if(_toFluidReaction == null)
            _toFluidReaction = new Queue<Fluid>();
        
        if (_toFluidReaction.Count == 0) 
            return this;
        
        return _toFluidReaction.Peek();
    }

    public Fluid GetLastReaction()
    {
        if(_toFluidReaction == null)
            _toFluidReaction = new Queue<Fluid>();
        
        if (_toFluidReaction.Count == 0) 
            return this;
        
        return _toFluidReaction.ToList().Last();
    }

    public bool CheckFinishReaction()
    {
        if (_color == _toFluidReaction.Peek().GetColor())
        {
            _toFluidReaction.Dequeue();
            
            if (_toFluidReaction.Count == 0)
                _isReaction = false;
            
            return true;
        }
        
        return false;
    }

    public Fluid Clone()
    {
        Fluid clone = new Fluid(_color,_density);
        clone.SetColor(_color);
        clone.SetDensity(_density);
        clone.SetIntensity(_intensity);
        clone.SetViscosity(_viscosity);
        clone.SetTimeToReaction(_time);
        clone.SetSpeedMixing(_speedMixing);
        return clone;
    }
}
