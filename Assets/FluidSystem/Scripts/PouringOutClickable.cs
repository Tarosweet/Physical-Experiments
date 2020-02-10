using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PouringOutClickable : MonoBehaviour
{
    [SerializeField] private Transform _pourPoint;
    [SerializeField] private ClickableObject _clickable;
    [SerializeField] private GameObject pourEffect;
    [SerializeField] private FluidContainer _fluidContainer;
    [SerializeField] private Transform _pouringObjectTransform;
    private PouringEffectParticle _currentPourEffect;
    [SerializeField] private float _speedOut = 1f;
    [SerializeField] private float _speedIn = 1f;
    [SerializeField] private float _size = 0.1f;
    private IFluidActionBuilder _builder;
    private Coroutine _coroutine;
    private void OnEnable()
    {
        _clickable.OnButtonDown += PourOut;
        _clickable.OnButtonUp += PourIn;
    }

    private void OnDisable()
    {
        _clickable.OnButtonDown -= PourOut;
        _clickable.OnButtonUp -= PourIn;
    }

    private void PourOut()
    {
        if(_fluidContainer.CheckWaterLevel(_pourPoint.position) && _fluidContainer.GetLitersFluid() > 0f)
        {
            float count = _speedOut;
            if (count > _fluidContainer.GetLitersFluid())
            {
                count = _fluidContainer.GetLitersFluid();
            }

            RaycastHit hit = GetHit();
            TransferFluid(hit, count);
        }
    }

    private RaycastHit GetHit()
    {
        Vector3 point = _pourPoint.position;

        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(point, Vector3.down * hit.distance, Color.yellow);
        }

        return hit;
    }

    private void TransferFluid(RaycastHit hit, float count)
    {
        FluidContainer container = hit.transform?.GetComponent<FluidContainer>();
        
        if(container == _fluidContainer)  container = null;
        
        if(_coroutine != null) 
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(TransferFluidDelay(container, count));
    }

    private IEnumerator TransferFluidDelay(FluidContainer container, float count)
    {
        float diff = count;
        while (diff > 0)
        {
            StartPouring(container);
            float newDiff = Mathf.MoveTowards(diff, 0, Time.deltaTime * count);
            float number = diff - newDiff;
            _builder = new DownTransferFluidActionBuilder(_fluidContainer, container, number);
            _builder.Build().Execute();
            diff = newDiff;
            yield return new WaitForSeconds(0.01f);
        }

        if (_fluidContainer.GetLitersFluid() < 0.001f)
        {
            _builder = new DownTransferFluidActionBuilder(_fluidContainer, container, _fluidContainer.GetLitersFluid());
            _builder.Build().Execute();
        }

        StopPouring();
    }

    private void StartPouring(FluidContainer container)
    {
        if (_currentPourEffect == null)
        {
            _currentPourEffect = Instantiate(pourEffect, _pourPoint.transform).GetComponent<PouringEffectParticle>();
        }
        
        if(container != null) 
            _currentPourEffect.SetColliderTrigger(container.GetCollider());
        
        _currentPourEffect.SetColor(_fluidContainer.GetColor(false));
        _currentPourEffect.SetPosition(_pourPoint.position);
        _currentPourEffect.SetSize(_size);
        _currentPourEffect.SetGravity(_pouringObjectTransform.lossyScale.y / 1000.0f);
    }

    private void StopPouring()
    {
        if (_currentPourEffect != null)
        {
           _currentPourEffect.Stop();
           _currentPourEffect = null;
        }
    }

    private void PourIn()
    {
        if (_fluidContainer.GetLitersFluid() == 0f)
        {
            RaycastHit hit = GetHit();
            TakeFluid(hit, _speedIn);
        }
    }

    private void TakeFluid(RaycastHit hit, float count)
    {
        if(hit.transform == null) return;
        FluidContainer container = hit.transform.GetComponent<FluidContainer>();
        
        if (container != null && container != _fluidContainer)
        {
            float liters = container.GetLitersFluid();
            if (liters < count) 
                count = liters;
            
            if(_coroutine != null) 
                StopCoroutine(_coroutine);
            
           _coroutine = StartCoroutine(TakeFluidDelay(container, count));
        }
    }

    private IEnumerator TakeFluidDelay(FluidContainer container, float count)
    {
        float diff = count;
        while (diff > 0)
        {
            float newDiff= Mathf.MoveTowards(diff, 0, Time.deltaTime * count);
            float number = diff - newDiff;
            
            _builder = new UpTransferFluidActionBuilder(container, _fluidContainer, number);
            _builder.Build().Execute();
            
            diff = newDiff;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
