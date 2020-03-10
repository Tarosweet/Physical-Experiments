using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class FluidContainer : MonoBehaviour
{
    [SerializeField] private GameObject _fluidObject;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _centerFluidLevelMaterial;
    [SerializeField] private float _maxLiters;
    [SerializeField] private float _countLiters;
    [SerializeField] private Collider _collider;
    [SerializeField] private List<Fluid> _fluids = new List<Fluid>();
    private float _percentFluid;
    private Transform _fluidTransform;
    
    private int MAX_COLORS = 15;

    public Action<Fluid> onAddedNewFluid;
    public Action onChangeCountLiters;
    
    private void Start()
    {
        Initialize();
    }
    
        
    private void Update()
    {
        ChangeDiffusion();
        //CalculateLiters();
        Filling();
    }
    
    private void Initialize()
    {

        if (_countLiters == 0)
            _fluidObject.SetActive(false);
        
        InitializeShader();
        CalculateAll();
        CalculateLiters();
        SubscribeFluids();
        Filling();
    }

    private void InitializeShader()
    {
        Color tmp = new Color(0,0,0,0);
        float t = 0;
        List<Color> colors = new List<Color>();
        List<float> floats = new List<float>();
        floats.Add(t);
        for (int i = 0; i < 15; i++)
        {
            colors.Add(tmp);
            floats.Add(0);
        }
        floats.Add(t);
        var rendererMaterial = _renderer.material;
        rendererMaterial.SetInt("_Count", 0);
        rendererMaterial.SetFloatArray("_Heights", floats);
        rendererMaterial.SetColorArray("_Color", colors);
    }
    
    private void SubscribeFluids()
    {
        foreach (var fluid in _fluids)
        {
            fluid.SetFluidContainer(this);
            SubscribeToFluid(fluid);
        }
    }

    public bool IsFull()
    {
        return Math.Abs(_maxLiters - _countLiters) < Mathf.Epsilon;
    }
    public float GetWaterRate()
    {
        return _maxLiters / CalculateHeight();
    }
    
    public float GetMaxLiters()
    {
        return _maxLiters;
    }

    private void CalculateLiters()
    {
        float liters = 0;
        foreach (var fluid in _fluids)
        {
            liters += fluid.GetCount();
        }
        
        if(liters > 0)
            _fluidObject.SetActive(true);
        
        if(liters <= 0)
            _fluidObject.SetActive(false);

        _countLiters = liters;

        if (onChangeCountLiters != null) 
            onChangeCountLiters.Invoke();
    }
    private void Filling()
    {
        CalculatePercent();
        float height = CalculateHeight();
        float level = height * _percentFluid;
        float middle = height / 2;
        _renderer.material.SetFloat("_FillAmount", middle - level + _centerFluidLevelMaterial);
    }

    private float CalculatePercent()
    { 
        _percentFluid = _countLiters / _maxLiters;
        return _percentFluid;
    }

    private float CalculateHeight()
    {
        var bounds = _renderer.bounds;
        return bounds.max.y - bounds.min.y;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    
    public bool CheckWaterLevel(Vector3 positionBottleneck)
    {
        return positionBottleneck.y <= this.GetWaterLevel() && _percentFluid > 0;
    }

    public float GetWaterLevel()
    {
        CalculatePercent();
        float height = CalculateHeight();
        float level = _renderer.bounds.min.y + height * _percentFluid;
        return level;
    }

    public float GetWaterMinLevel()
    {
        return _renderer.bounds.min.y;
    }
    
    public float GetWaterMaxLevel()
    {
        return _renderer.bounds.max.y;
    }
    
    public float GetLitersFluid()
    {
        return _countLiters;
    }

    public float GetAverageDensity()
    {
        if (_fluids.Count == 0) return 0;
        
        float density = 0;
        
        foreach (var fluid in _fluids)
        {
            density += fluid.GetDensity();
        }

        float average = density / _fluids.Count;
        
        return average;
    }

    public Color GetColor(bool isUp)
    {
        if (_fluids.Count == 0) return new Color(0,0,0,0);
        return isUp ? _fluids[_fluids.Count - 1].GetColor() : _fluids[0].GetColor();
    }

    private void CalculateAll()
    {
        var rendererMaterial = _renderer.material;
        List<float> heights = new List<float>();
        List<float> diffusions = new List<float>();
        List<Color> colours = new List<Color>();
        float nextPercent = 0;
        
        var bounds = _renderer.bounds;
        float minPos = bounds.center.y - bounds.min.y;
        float maxPos = bounds.max.y - bounds.center.y;
        float height = CalculateHeight();
        float currentPos = -minPos;
        
        heights.Add(currentPos);

        for (int i = 0; i < _fluids.Count; i++)
        {
            float percent = (_fluids[i].GetCount() / _maxLiters) * height;
            currentPos += percent;
            heights.Add(currentPos);
            diffusions.Add(_fluids[i].GetDiffusion());
            colours.Add(_fluids[i].GetColor());
        }

        for (int i = 0; i < MAX_COLORS - _fluids.Count; i++)
        {
            heights.Add(currentPos + 10);
            diffusions.Add(0);
            colours.Add(new Color(0, 0, 0, 0));
        }
        
//        Debug.Log("heitght:" + height);
//        Debug.Log("maxPos:" + maxPos);
//        string s = "";
//        foreach (float h in heights)
//        {
//            s += h.ToString() + " ";
//        }
//        Debug.Log(s);
        
        rendererMaterial.SetInt("_Count", _fluids.Count + 1);
        rendererMaterial.SetFloatArray("_Heights", heights);
        rendererMaterial.SetFloatArray("_RangeDiffusion",diffusions);
        rendererMaterial.SetColorArray("_Color", colours);
    }

    private void ChangeFluids()
    {
        CalculateAll();
    }
    
    private void ChangeCounts()
    {
        var rendererMaterial = _renderer.material;
        rendererMaterial.SetInt("_Count", _fluids.Count + 1);
    }
    
    private void ChangeColours()
    {
        List<Color> colours = new List<Color>();
        var rendererMaterial = _renderer.material;
        
        for (int i = 0; i < _fluids.Count; i++)
        {
            colours.Add(_fluids[i].GetColor());
        }

        for (int i = 0; i < MAX_COLORS - _fluids.Count; i++)
        {
            colours.Add(new Color(0, 0, 0, 0));
        }
        
        rendererMaterial.SetColorArray("_Color", colours);
    }

    private void ChangeHeights()
    {
        var rendererMaterial = _renderer.material;
        List<float> heights = new List<float>();
        float nextPercent = 0;
        
        var bounds = _renderer.bounds;
        float minPos = bounds.center.y - bounds.min.y;
        float maxPos = bounds.max.y - bounds.center.y;
        float height = CalculateHeight();
        float currentPos = -minPos;
        for (int i = 0; i < _fluids.Count; i++)
        {
            float percent = (_fluids[i].GetCount() / _maxLiters) * height;
            currentPos += percent;
            heights.Add(currentPos);
        }
        
        for (int i = 0; i < MAX_COLORS - _fluids.Count; i++)
        {
            heights.Add(currentPos + 10);
        }
        
        rendererMaterial.SetFloatArray("_Heights", heights);
    }

    private void ChangeDiffusion()
    {
        List<float> diffusions = new List<float>();
        
        var rendererMaterial = _renderer.material;
        
        for (int i = 0; i < _fluids.Count; i++)
        {
            diffusions.Add(_fluids[i].GetDiffusion());
        }

        for (int i = 0; i < MAX_COLORS - _fluids.Count; i++)
        {
            diffusions.Add(0);
        }
        
        rendererMaterial.SetFloatArray("_RangeDiffusion", diffusions);
    }

    public Collider GetCollider()
    {
        return _collider;
    }

    public List<Fluid> GetFluids()
    {
        return _fluids;
    }

    public void SetFluids(List<Fluid> fluids)
    {
        _fluids = fluids;
    }
    
    public void MixingWithTime(Fluid from)
    {
        StartCoroutine(MixingWithTimeDelay(from));
    }

    private IEnumerator MixingWithTimeDelay(Fluid from)
    {
        Fluid toFluid = from.GetReactionMixing();
        yield return new WaitForSeconds(toFluid.GetTimeToReaction());
        
        while (from.GetStatusReactionMixing())
        {
            from.SetColor(Color.Lerp(from.GetColor(), toFluid.GetColor(), Time.deltaTime * toFluid.GetSpeedMixing()));
            toFluid = from.GetReactionMixing();
            if (from.CheckFinishReactionMixing())
            {
                from.SetColor(toFluid.GetColor());
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void DiffusionWithTime(Fluid fluid)
    {
        StartCoroutine(DiffusionWithTimeDelay(fluid));
    }

    private IEnumerator DiffusionWithTimeDelay(Fluid fluid)
    {
        fluid.StartReactionDiffusion();
        yield return new WaitForSeconds(fluid.GetTimeToDiffusion());
        float currentDiffusion = fluid.GetDiffusion();
        float toDiffusion = fluid.GetFinalDiffusion();
        while (Math.Abs(currentDiffusion - toDiffusion) > Mathf.Epsilon)
        {
            fluid.SetDiffusion(Mathf.MoveTowards(currentDiffusion,toDiffusion,Time.deltaTime * fluid.GetSpeedDiffusion()));
            currentDiffusion = fluid.GetDiffusion();
            yield return new WaitForSeconds(0.01f);
        }
        fluid.SetDiffusion(toDiffusion);
        fluid.StopReactionDiffusion();

        if (fluid.IsMergeAfterDiffusion())
        {
            int index = _fluids.IndexOf(fluid);
            if (index + 1 < _fluids.Count)
            {
                Fluid fluidTo = _fluids[index + 1];
                MergeFluids(fluid, fluidTo);
            }
        }
    }

    public void MergeFluids(Fluid fluidFrom, Fluid fluidTo)
    {
        StartCoroutine(MergeFluidsWithTime(fluidFrom, fluidTo));
    }

    private IEnumerator MergeFluidsWithTime(Fluid fluidFrom, Fluid fluidTo)
    {
        float maxCount = fluidFrom.GetCount();
        float count = fluidFrom.GetCount() * fluidFrom.GetSpeedMerge();
        fluidFrom.StartMergeReaction();
        
        float currentDiffusion = fluidFrom.GetDiffusion();
        float toDiffusion = 0;
        
        while (fluidFrom.GetCount() > 0)
        {
            count = maxCount * fluidFrom.GetSpeedMerge() * Time.deltaTime;
            IFluidAction action = new DecreaseFluidAction(this, fluidFrom, count);
            action.Execute();
            action = new IncreaseFluidAction(this, fluidTo, count);
            action.Execute();
            action = new MixingFluidAction(this, fluidTo, fluidFrom, count);
            action.Execute();
            fluidFrom.SetDiffusion(Mathf.MoveTowards(currentDiffusion,toDiffusion,Time.deltaTime * fluidFrom.GetSpeedMerge()));
            currentDiffusion = fluidFrom.GetDiffusion();
            yield return new WaitForSeconds(0.01f);
        }
        fluidFrom.StopMergeReaction();
    }
    
    public void AppendFluid(Fluid fluid)
    {
        float density = fluid.GetDensity();
        int i;
        for (i = 0; i < _fluids.Count; i++)
        {
            if (_fluids[i].GetDensity() < density)
            {
                break;
            }
        }
        
        _fluids.Insert(i,fluid);
        SubscribeToFluid(fluid);
        ChangeFluids();

        if (onAddedNewFluid != null) 
            onAddedNewFluid.Invoke(fluid);
    }
    
    private void RemoveFluid(Fluid fluid)
    {
        UnsubscribeToFluid(fluid);
        _fluids.Remove(fluid);
        ChangeFluids();
    }

    public void OnChangeColor()
    {
        CalculateAll();
    }
    
    public void OnChangeLiters()
    {
        CalculateLiters();
        CalculateAll();
    }

    public void OnChangeDiffusion()
    {
        CalculateAll();
    }

    public void OnDeletedFluid()
    {
        OnChangeColor();
        OnChangeLiters();
    }
    
    public void SubscribeToFluid(Fluid fluid)
    {
        fluid.onChangeColor += OnChangeColor;
        fluid.onChangeCount += OnChangeLiters;
        fluid.onZeroCount += RemoveFluid;
        fluid.onChangeDiffusion += OnChangeDiffusion;
    }

    public void UnsubscribeToFluid(Fluid fluid)
    {
        fluid.onChangeColor -= OnChangeColor;
        fluid.onChangeCount -= OnChangeLiters;
        fluid.onZeroCount -= RemoveFluid;
        fluid.onChangeDiffusion -= OnChangeDiffusion;
    }
}
