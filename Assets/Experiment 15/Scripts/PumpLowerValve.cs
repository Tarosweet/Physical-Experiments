using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpLowerValve : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private PumpHandle _pumpHandle;
    [SerializeField] private Vector3 _upperPosition;
    [SerializeField] private Vector3 _lowerPosition;

    private void OnEnable()
    {
        _pumpHandle.onHandDown += MoveUp;
        _pumpHandle.onHandUp += MoveDown;
    }

    private void OnDisable()
    {
        _pumpHandle.onHandDown -= MoveUp;
        _pumpHandle.onHandUp -= MoveDown;
    }

    private void MoveUp(float percent)
    {
        Vector3 direction = (_upperPosition - _lowerPosition).normalized;
        float distance = Vector3.Distance(_upperPosition, _lowerPosition);
        _transform.localPosition = _lowerPosition + direction * (distance * (1-percent));
    }
    
    private void MoveDown(float percent)
    {
        _transform.localPosition = _upperPosition;
    }

    public float GetPercent()
    {
        float maxRange = Vector3.Distance(_lowerPosition, _upperPosition);
        float distance = Vector3.Distance(_lowerPosition, _transform.localPosition);
        return distance / maxRange;
    }
}
