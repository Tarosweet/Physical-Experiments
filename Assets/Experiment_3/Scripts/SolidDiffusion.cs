using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidDiffusion : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private float _speedDiffuasion;
    [SerializeField] private float _maxDiffusion;

    private float _diffusionValue = 0;

    public Action OnEndDiffusion;
    public void SetTopColor(Color color)
    {
        _renderer.material.SetColor("_Color2", color);
    }
    
    
    public void SetBottomColor(Color color)
    {
        _renderer.material.SetColor("_Color1", color);
    }

    public void SetDiffusionValue(float value)
    {
        _diffusionValue = value;
        _renderer.material.SetFloat("_Diffusion", _diffusionValue);
    }

    public void StartDiffusion()
    {
        StartCoroutine(DiffusionSmooth());
    }

    private IEnumerator DiffusionSmooth()
    {
        while (_diffusionValue < _maxDiffusion)
        {
            SetDiffusionValue(Mathf.MoveTowards(_diffusionValue, _maxDiffusion, Time.deltaTime * _speedDiffuasion));
            yield return new WaitForSeconds(0.01f);
        }
        
        OnEndDiffusion?.Invoke();
    }
}
