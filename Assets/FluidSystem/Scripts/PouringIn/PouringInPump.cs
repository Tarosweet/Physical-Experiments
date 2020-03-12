using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringInPump : MonoBehaviour
{
    [SerializeField] private Transform _pourPoint;
    [SerializeField] private PumpHandle _pumpHandle;
    [SerializeField] private PumpLowerValve _pumpLowerValve;
    [SerializeField] private FluidContainer _fluidContainer;
    [SerializeField] private float _speedIn = 1f;

    private IFluidActionBuilder _builder;

    
    private void OnEnable()
    {
        _pumpHandle.onHandDown += PourIn;
    }

    private void OnDisable()
    {
        _pumpHandle.onHandDown -= PourIn;
    }

    private void PourIn(float percent)
    {
        if (_pumpLowerValve.GetPercent() > 0.01f)
        {
            RaycastHit hit = GetHit();
            TakeFluid(hit, _speedIn);
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
    
    private void TakeFluid(RaycastHit hit, float count)
    {
        if(hit.transform == null) return;
        FluidContainer container = hit.transform.GetComponent<FluidContainer>();
        
        if (container != null && container != _fluidContainer)
        {
            float liters = container.GetLitersFluid();
            if (liters < count) 
                count = liters;

            float maxLiters = _fluidContainer.GetMaxLiters() - _fluidContainer.GetLitersFluid();
            if (count > maxLiters)
            {
                count = maxLiters;
            }
            
            _builder = new UpTransferFluidActionBuilder(container, _fluidContainer, count);
            _builder.Build().Execute();
        }
    }
}
