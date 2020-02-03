﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringOut : MonoBehaviour
{
    [SerializeField] private MeshRenderer pourPoint;

    [SerializeField] private GameObject pourEffect;

    [SerializeField] private float angleToPour = 30;

    [SerializeField] private float speed = 1f;

    [SerializeField] private FluidContainer _fluidContainer;

    private Transform _pouringObjectTransform;

    private PouringEffectParticle _currentPourEffect;

    private IPouring _pouring;

    void Start()
    {
        _pouringObjectTransform = transform;
        _pouring = GetComponent<IPouring>();
        
    }
    
    void Update()
    {
        Debug.DrawLine(pourPoint.bounds.min, pourPoint.bounds.max, Color.red);
        if (_fluidContainer.CheckWaterLevel(pourPoint.bounds.min))
        {
            float count = speed;
            if (speed > _fluidContainer.GetLitersFluid())
            {
                count = _fluidContainer.GetLitersFluid();
            }
            _fluidContainer.Decrease(count);
            TransferFluid(PourOut(), count);
            return;
        }

        StopPouring();
    }

    private RaycastHit PourOut()
    {
        var bounds = pourPoint.bounds;
        Vector3 point = CalculatePosition();

        if (!_currentPourEffect)
        {
            _currentPourEffect = Instantiate(pourEffect, pourPoint.transform).GetComponent<PouringEffectParticle>();
        }

        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, out hit, Mathf.Infinity))
        {
            float size = CalculateSize();
            Debug.DrawRay(point, Vector3.down * hit.distance, Color.yellow);
            _currentPourEffect.SetColor(_fluidContainer.GetColor());
            _currentPourEffect.SetPosition(point);
            _currentPourEffect.SetSize(size);
            _currentPourEffect.SetGravity(_pouringObjectTransform.lossyScale.y/1000.0f);
        }

        return hit;
    }

    private float CalculateSize()
    {
        var bounds = pourPoint.bounds;
        if (_fluidContainer.GetWaterLevel() > (bounds.size.y + bounds.min.y))
        {
            float dist = Vector3.Distance(bounds.min, bounds.max); 
            return Mathf.Sqrt(dist * dist / 2);
        }

        Vector3 boundMin = bounds.min;
        Vector3 to = new Vector3(boundMin.x, _fluidContainer.GetWaterLevel(), boundMin.z);
        return Vector3.Distance(boundMin, to);
    }

    private Vector3 CalculatePosition()
    {
        var bounds = pourPoint.bounds;
        Vector3 center = bounds.center;
        Vector3 boundMin = bounds.min;
        Vector3 down = new Vector3(center.x, boundMin.y, center.z);
        if (_fluidContainer.GetWaterLevel() > (bounds.max.y))
        {
            return bounds.center;
        }
        Vector3 pos = transform.position;
        float h = Vector3.Distance(center, down);
        h *= h;
        float incline = (center - pos).normalized.y;
        float radius = Vector3.Distance(down, boundMin);
        radius *= radius;
        float distance;
        if (radius > h)
        {
            distance = Mathf.Sqrt(radius - h);
        }
        else
        {
            distance = -Mathf.Sqrt(h - radius);
        }
        if (incline <= 0) distance = -distance;
        Vector3 result = down + (new Vector3(down.x, pos.y, down.z) - pos).normalized * distance;
        Vector3 level = new Vector3(boundMin.x, _fluidContainer.GetWaterLevel(), boundMin.z);
        float distanceLevel = Vector3.Distance(boundMin, level) / 2;
        Vector3 to = (center - result).normalized * distanceLevel;
        result += to;
       /* if (Vector3.Distance(result, down) > distanceLevel)
        {
            result = result + (down - result).normalized * distanceLevel;
        }*/

        return result;
    }

    private void TransferFluid(RaycastHit hit, float count)
    {
        if(hit.transform == null) return;
        
        FluidContainer container = hit.transform.GetComponent<FluidContainer>();
        if (container != null && container != _fluidContainer)
        {
            _currentPourEffect.SetColliderTrigger(container.GetCollider());
            container.Increase(count, _fluidContainer.GetColor());
        }
    }

    private void StopPouring()
    {
        if (_currentPourEffect != null)
        {
            StartCoroutine(StopPouringDelay());
        }
    }

    IEnumerator StopPouringDelay()
    {
        _currentPourEffect.Stop();
        GameObject tmp = _currentPourEffect.gameObject;
        float time = _currentPourEffect.GetDelay();
        _currentPourEffect = null;
        yield return new WaitForSeconds(time);
        Destroy(tmp);
    }
}
