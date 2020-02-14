using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringOutClickableFaucet : MonoBehaviour
{
    [SerializeField] private Transform _pourPoint;
    [SerializeField] private ClickableObject _clickable;
    [SerializeField] private GameObject pourEffect;
    [SerializeField] private FluidContainer _fluidContainer;
    [SerializeField] private Transform _pouringObjectTransform;
    private PouringEffectParticle _currentPourEffect;
    [SerializeField] private float _speedOut = 1f;
    [SerializeField] private float _size = 0.1f;
    [SerializeField] private FaucetHandle _handle;
    private bool isActive = false;
    private IFluidActionBuilder _builder;

    private void Start()
    {
        _handle.SetColor(_fluidContainer.GetColor(true));
    }

    private void OnEnable()
    {
        _clickable.OnButtonDown += Activate;
    }

    private void OnDisable()
    {
        _clickable.OnButtonDown -= Activate;
    }

    private void Activate()
    {
        isActive = !isActive;
        if (isActive)
        {
            StartPouring();
        }
        else
        {
            StopPouring();
        }
    }

    private void Update()
    {
        if(isActive)
            PourOut();
    }

    private void PourOut()
    {
        RaycastHit hit = GetHit();
        TransferFluid(hit, _speedOut);
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
        if (hit.transform == null) return;
        FluidContainer container = hit.transform.GetComponent<FluidContainer>();
        
        if (container != null)
        {
            _currentPourEffect.SetColliderTrigger(container.GetCollider());
            _builder = new InfinityTransferActionBuilder(_fluidContainer, container, count);
            _builder.Build().Execute();
        }
    }

    private void StartPouring()
    {
        if (_currentPourEffect == null)
        {
            _currentPourEffect = Instantiate(pourEffect, _pourPoint.transform).GetComponent<PouringEffectParticle>();
        }

        _currentPourEffect.SetColor(_fluidContainer.GetColor(true));
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

}
