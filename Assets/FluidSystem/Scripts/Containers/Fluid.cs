using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Fluid
{
    [Header("Основное")]
    [Tooltip("Цвет вещества")]
    [SerializeField] private Color _color;
    
    [Tooltip("Количество вещества")]
    [SerializeField] private float _count;
    
    [Min(1)]
    [Tooltip("Интенсивность цвета")]
    [SerializeField] private float _intensity = 1;
    
    [Tooltip("Плотность вещества")]
    [SerializeField] private float _density;
    
    [Tooltip("Вязкозть вещества")]
    [SerializeField] private float _viscosity;

    [Tooltip("Начальная диффузия")]
    [Range(0,1)]
    [SerializeField] private float _diffusion;

    [Header("Смена цвета по времени")] 
    
    [SerializeField] private bool _isTimeMixing;
    
    [Tooltip("Время до реакции")]
    [SerializeField] private float _time;
    
    [Tooltip("Скорость реакции")]
    [SerializeField] private float _speedMixing;
    
    [Header("Диффузия")]
    [SerializeField] private bool _isDiffusion;
    [Range(0,1)]
    [Tooltip("Максимальная диффузия")]
    [SerializeField] private float _finalDiffusion;
    [Tooltip("Время до диффузии")] 
    [SerializeField] private float _timeToDiffusion;
    [Tooltip("Скорость диффузии")]
    [SerializeField] private float _speedDiffusion;
    [Tooltip("Объединить после диффузии. Объединяет с жидкостью выше.")]
    [SerializeField] private bool _mergeAfterDiffusion;
    [Tooltip("Скорость объединения")]
    [Range(0.000001f, 1f)]
    [SerializeField] private float _speedMerge;
    
    [Space(10)]
    [SerializeField] private bool _isReactionMixing = false;
    [SerializeField] private bool _isReactionDiffusion = false;
    [SerializeField] private bool _isReactionMerge = false;
    
    private Fluid _toFluidMixingTime;
    private Fluid _toFluidDiffusionTime;
    
    private FluidContainer _container;
    
    public Action onChangeColor;
    public Action onChangeCount;
    public Action onChangeDiffusion;
    public Action<Fluid> onZeroCount;

    public Fluid(Color color, float density)
    {
        _color = color;
        _count = 0;
        _intensity = 1;
        _time = 0;
        _density = density;
        _viscosity = 1;
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

    public float GetDiffusion()
    {
        return _diffusion;
    }

    public void SetDiffusion(float diffusion)
    {
        _diffusion = diffusion;
        
        if (onChangeDiffusion != null) 
            onChangeDiffusion.Invoke();
    }

    public bool IsDiffusion()
    {
        return _isDiffusion;
    }

    public void SetIsDiffusion(bool isDiffusion)
    {
        _isDiffusion = isDiffusion;
    }

    public float GetTimeToDiffusion()
    {
        return _timeToDiffusion;
    }

    public void SetTimeToDiffusion(float time)
    {
        _timeToDiffusion = time;
    }

    public float GetSpeedDiffusion()
    {
        return _speedDiffusion;
    }

    public void SetSpeedDiffusion(float speed)
    {
        _speedDiffusion = speed;
    }

    public float GetFinalDiffusion()
    {
        return _finalDiffusion;
    }

    public void SetFinalDiffusion(float diffusion)
    {
        _finalDiffusion = diffusion;
    }

    public void StartReactionDiffusion()
    {
        _isReactionDiffusion = true;
    }

    public void StopReactionDiffusion()
    {
        _isReactionDiffusion = false;
    }

    public bool GetStatusReactionDiffusion()
    {
        return _isReactionDiffusion;
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

    public void SetTimeToReactionMixing(float time)
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

    public bool IsTimeMixing()
    {
        return _isTimeMixing;
    }

    public void SetIsTimeMixing(bool isTime)
    {
        _isTimeMixing = isTime;
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
        _toFluidMixingTime = fluid;
        _isReactionMixing = true;
    }

    public bool GetStatusReactionMixing()
    {
        return _isReactionMixing;
    }

    public Fluid GetReactionMixing()
    {
        if (_toFluidMixingTime == null) 
            return this;

        return _toFluidMixingTime;
    }

    public Fluid GetLastReactionMixing()
    {
        if (_toFluidMixingTime == null) 
            return this;

        return _toFluidMixingTime;
    }

    public bool CheckFinishReactionMixing()
    {
        if (_color == _toFluidMixingTime.GetColor())
        {

            _toFluidMixingTime = null;
            _isReactionMixing = false;
            return true;
        }

        return false;
    }

    public float GetSpeedMerge()
    {
        return _speedMerge;
    }

    public void SetSpeedMerge(float speed)
    {
        _speedMerge = speed;
    }
    
    public bool IsMergeAfterDiffusion()
    {
        return _mergeAfterDiffusion;
    }

    public void SetMergeAfterDiffusion(bool merge)
    {
        _mergeAfterDiffusion = merge;
    }
    public void StartMergeReaction()
    {
        _isReactionMerge = true;
    }

    public void StopMergeReaction()
    {
        _isReactionMerge = false;
    }

    public bool GetStatusReactionMerge()
    {
        return _isReactionMerge;
    }

    public void SetFluidContainer(FluidContainer container)
    {
        _container = container;
        SubscribeToContainer();
    }

    private void SubscribeToContainer()
    {
        if (_isDiffusion)
        {
            _container.onAddedNewFluid += OnDiffusionAfterAddded;
        }
    }

    public FluidContainer GetFluidContainer()
    {
        return _container;
    }

    private void OnDiffusionAfterAddded(Fluid fluid)
    {
        List<Fluid> fluids = _container.GetFluids();
        int pos = fluids.IndexOf(this);
        int posAdded = fluids.IndexOf(fluid);
        int diff = posAdded - pos;
        if (diff == 1)
        {
            IFluidAction action = new DiffusionDelayAction(_container, this);
            action.Execute();
        }
    }

    public Fluid Clone()
    {
        Fluid clone = new Fluid(_color,_density);
        clone.SetColor(_color);
        clone.SetIntensity(_intensity);
        clone.SetDensity(_density);
        clone.SetViscosity(_viscosity);
        clone.SetDiffusion(_diffusion);
        clone.SetTimeToReactionMixing(_time);
        clone.SetIsTimeMixing(_isTimeMixing);
        clone.SetSpeedMixing(_speedMixing);
        clone.SetIsDiffusion(_isDiffusion);
        clone.SetFinalDiffusion(_finalDiffusion);
        clone.SetTimeToDiffusion(_timeToDiffusion);
        clone.SetSpeedDiffusion(_speedDiffusion);
        return clone;
    }
}
