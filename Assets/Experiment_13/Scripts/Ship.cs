using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;

    private Coroutine _moveCoroutine;
    public Action moveStart;
    public Action moveEnd;

    public Vector3 GetPosition()
    {
        return _transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }
    
    public void MoveTo(Vector3 pos)
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
        
        _moveCoroutine = StartCoroutine(MoveToSmooth(pos));
    }

    private IEnumerator MoveToSmooth(Vector3 pos)
    {
        moveStart?.Invoke();
        
        float distance = Vector3.Distance(_transform.position, pos);
        
        while (distance > 0)
        {
            var position = _transform.position;
            position = Vector3.MoveTowards(position, pos, Time.deltaTime * _speed);
            _transform.position = position;
            distance = Vector3.Distance(position, pos);
            yield return new WaitForSeconds(0.01f);
        }

        _transform.position = pos;
        moveEnd?.Invoke();
    }
    
    
}
