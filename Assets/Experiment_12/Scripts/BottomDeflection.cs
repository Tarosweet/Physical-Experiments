using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDeflection : MonoBehaviour
{
    [SerializeField] private Transform _bottomObjectTransform;
    [SerializeField] private Vector3 _negativePosition;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private Vector3 _negativeScale;
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private Vector3 _endScale;
    [Range(0f,1f)]
    [SerializeField] private float _percent = 0f;

    [SerializeField] private FluidContainer _container;
    [SerializeField] private Wobble _wobble;
    [SerializeField] private float _startTimeRecovery = 0.1f;
    [SerializeField] private float _endTimeRecovery = 5f;

    [SerializeField] private FluidContainer _interactContainer;
    [SerializeField] private bool _isInteractWaterLevel;
    private void Start()
    {
        SetPosition(_startPosition);
        SetScale(_startScale);
    }

    private void OnEnable()
    {
        _container.onChangeCountLiters += CalculateStretching;
    }

    private void OnDisable()
    {
        _container.onChangeCountLiters -= CalculateStretching;
    }

    private void Update()
    {
        CalculateStretching();
    }

    private void SetPosition(Vector3 pos)
    {
        _bottomObjectTransform.localPosition = pos;
    }

    private void SetScale(Vector3 scale)
    {
        _bottomObjectTransform.localScale = scale;
    }

    private void CalculatePercent()
    {
        if (_percent >= 0)
        {
            SetPosition(_startPosition + (_endPosition - _startPosition) * _percent);
            SetScale(_startScale + (_endScale - _startScale) * _percent);
        }
        else
        {
            SetPosition(_startPosition - (_negativePosition - _startPosition) * _percent);
            SetScale(_startScale - (_negativeScale - _startScale) * _percent);
        }
    }

    private void CalculateStretching()
    {
        float countLiters = _container.GetLitersFluid();
        float maxCountLiters = _container.GetMaxLiters();
        float percent = countLiters / maxCountLiters * _container.GetAverageDensity();

        if (_isInteractWaterLevel)
        {
            if (_container.GetWaterMinLevel() < _interactContainer.GetWaterLevel())
            {
                float distance = _container.GetWaterLevel() - _container.GetWaterMinLevel(); 
                if (_container.GetWaterLevel() <= _interactContainer.GetWaterLevel())
                {
                    distance = _container.GetWaterMaxLevel() - _container.GetWaterMinLevel();
                    percent = 0 - (_interactContainer.GetWaterLevel() - _container.GetWaterLevel()) / distance;
                }
                else
                {
                    percent *= (_container.GetWaterLevel() - _interactContainer.GetWaterLevel()) / distance;
                }
            }
        }
            
        _percent = Mathf.Clamp(percent, -1, 1);

        if (_percent > 0f)
        {
            SetRecovery(_endTimeRecovery);
        }
        else
        {
            SetRecovery(_startTimeRecovery);
        } 
        
        CalculatePercent();
    }

    private void SetRecovery(float time)
    {
        _wobble.SetRecovery(time);
    }

    public void StartInteractWaterLevel(FluidContainer container)
    {
        _interactContainer = container;
        _isInteractWaterLevel = true;
    }

    public void StopInteractWaterLevel()
    {
        _isInteractWaterLevel = false;
        _interactContainer = null;
    }
    
}
