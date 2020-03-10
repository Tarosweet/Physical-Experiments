using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private Vector3 _endRotation;
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _speedRotation;
    [SerializeField] private bool _isInverse;
    [SerializeField] private bool _isLoopRotation;
    [SerializeField] private bool _isEnd;

    private Coroutine _coroutine;

    public Action OnChanged;
    
    public bool IsEnd()
    {
        return _isEnd;
    }

    public void Play()
    {
        Vector3 endPos = _isEnd ? _startPosition : _endPosition;
        Vector3 endRotation = _isEnd? _startRotation : _endRotation;
        
        if (_isLoopRotation) endRotation = _endRotation;
        
        int inverse = _isInverse && _isEnd ? -1 : 1;
        
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        
        _coroutine = StartCoroutine(PlaySmooth(endPos, endRotation, inverse));
    }

    private IEnumerator PlaySmooth(Vector3 endPos,Vector3 endRotation,int inverse)
    {
        float distance = Vector3.Distance(_transform.localPosition, endPos);
        
        Vector3 DecreaseAngle = _transform.localEulerAngles - endRotation;
        DecreaseAngle = new Vector3(Mathf.Abs(DecreaseAngle.x),
                                    Mathf.Abs(DecreaseAngle.y),
                                    Mathf.Abs(DecreaseAngle.z));
        
        Vector3 direction = (endRotation - _transform.localEulerAngles).normalized;
        
        float angle = Vector3.Distance(DecreaseAngle, Vector3.zero);
        float prevAngle = angle + 1;
        
        while (distance > 0 || prevAngle > angle)
        {
            Vector3 localPosition = _transform.localPosition;

            localPosition =
                Vector3.MoveTowards(localPosition, endPos, Time.deltaTime * _speedMovement);
            _transform.localPosition = localPosition;

            if (prevAngle > angle)
            {
                float angleMinus = Time.deltaTime * _speedRotation;
                DecreaseAngle -= angleMinus * direction;
                _transform.Rotate(direction, angleMinus * inverse);

                prevAngle = angle;
                angle = Vector3.Distance(DecreaseAngle, Vector3.zero);
            }

            distance = Vector3.Distance(localPosition, endPos);
            yield return new WaitForSeconds(0.01f);
        }

        _transform.localPosition = endPos;
        _transform.localEulerAngles = endRotation;
        
        OnChanged?.Invoke();
        
        _isEnd = !_isEnd;
    }
}
