using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class FluidContainer : MonoBehaviour
{
    [SerializeField] private GameObject _fluidObject;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _maxLiters;
    [SerializeField] private float _countLiters;
    [SerializeField] private Color _color;
    [SerializeField] private Collider _collider;
    [SerializeField] private List<Fluid> _fluids = new List<Fluid>();
    private float _percentFluid;
    
    private void Start()
    {
        Initialize();
        CalculateColours();
    }
    
        
    public void Update()
    {
        CalculateLiters();
        Filling();
        CalculateColours();
    }

    private void Initialize()
    {

        if (_countLiters == 0)
            _fluidObject.SetActive(false);
        
        InitializeShader();
        CalculateColours();
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
            SubscribeToFluid(fluid);
        }
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
    }
    private void Filling()
    {
        CalculatePercent();
        float height = CalculateHeight();
        float level = height * _percentFluid;
        float middle = height / 2;
        _renderer.material.SetFloat("_FillAmount", middle - level + 0.5f);
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

    public float GetLitersFluid()
    {
        return _countLiters;
    }

    public Color GetColor(bool isUp)
    {
        if (_fluids.Count == 0) return new Color(0,0,0,0);
        return isUp ? _fluids[_fluids.Count - 1].GetColor() : _fluids[0].GetColor();
    }

    private void CalculateColours()
    {
        var rendererMaterial = _renderer.material;
        List<float> heights = new List<float>();
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
            colours.Add(_fluids[i].GetColor());
        }
        
        heights.Add(currentPos + 10);
        colours.Add(new Color(0, 0, 0, 0));

        rendererMaterial.SetInt("_Count", _fluids.Count + 1);
        rendererMaterial.SetFloatArray("_Heights", heights);
        rendererMaterial.SetColorArray("_Color", colours);
    }

    public void Decrease(float count)
    {
        float diff = _countLiters - count;
        if (diff <= 0)
        {
            _countLiters = 0;
            _fluidObject.SetActive(false);
        }
        else
        {
            _countLiters = diff;
        }
    }

    public void Increase(float count, Color color)
    {
        float sum = _countLiters + count;
        if (_countLiters <= 0f)
        {
            //SetColor(color);
            _fluidObject.SetActive(true);
        }
        
        if (sum >= _maxLiters)
        {
            _countLiters = _maxLiters;
        }
        else
        {
            _countLiters = sum;
        }

        if (_countLiters - count > 0)
        {
            //SetColor(CalculateColor(color, count));
        }
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
        int index = _fluids.IndexOf(from);
        from = _fluids[index];
        
        Fluid toFluid = from.GetReaction();
        yield return new WaitForSeconds(toFluid.GetTimeToReaction());
        
        while (from.GetStatusReaction())
        {
            from.SetColor(Color.LerpUnclamped(from.GetColor(), toFluid.GetColor(), Time.deltaTime * toFluid.GetSpeedMixing()));
            if (from.CheckFinishReaction())
            {
                from.SetColor(toFluid.GetColor());
                toFluid = from.GetReaction();
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void RemoveFluid(Fluid fluid)
    {
        UnsubscribeToFluid(fluid);
        _fluids.Remove(fluid);
    }

    public void OnChangeColor()
    {
        CalculateColours();
    }
    
    public void OnChangeLiters()
    {
        CalculateLiters();
        CalculateColours();
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
    }

    public void UnsubscribeToFluid(Fluid fluid)
    {
        fluid.onChangeColor -= OnChangeColor;
        fluid.onChangeCount -= OnChangeLiters;
        fluid.onZeroCount -= RemoveFluid;
    }
}
