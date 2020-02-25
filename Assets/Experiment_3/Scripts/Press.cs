using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    [SerializeField] private ClickableObject _clickableObject;
    [SerializeField] private List<MovableObject> _movableObjects;

    private Coroutine _coroutine;
    
    private int _changedCount;

    public Action OnToEndPositionOfAnimation;
    public Action OnToStartPositionOfAnimation;
    private void OnEnable()
    {
        _clickableObject.OnButtonDown += Interact;
        
        foreach (var movableObject in _movableObjects)
        {
            movableObject.OnChanged += OnChangedIncrease;
        } 
    }

    private void OnDisable()
    {
        _clickableObject.OnButtonDown -= Interact;
        
        foreach (var movableObject in _movableObjects)
        {
            movableObject.OnChanged -= OnChangedIncrease;
        }
    }
    
    private void Interact()
    {
        if (_coroutine != null)
        {
            return;
        }
        
        _changedCount = 0;
        _coroutine = StartCoroutine(InteractDelay());
    }

    private IEnumerator InteractDelay()
    {
        foreach (var movableObject in _movableObjects)
        {
            movableObject.Play();
        }

        while (_changedCount < _movableObjects.Count)
        {
            yield return new WaitForSeconds(0.01f);
        }

        if (_movableObjects[0].IsEnd())
        {
            OnToEndPositionOfAnimation?.Invoke();
        }
        else
        {
            OnToStartPositionOfAnimation?.Invoke();
        }

        _coroutine = null;
    }

    private void OnChangedIncrease()
    {
        _changedCount++;
    }
}
