using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : MonoBehaviour
{
    [Serializable]
    public class LockedVariable
    {
        public float value;
        public float startValue;
        public bool isLocked;
        public bool clamp;
        public float min, max;
        
        public float LockVariable()
        {
            if (!isLocked)
                return value;

            if (clamp)
                return Mathf.Clamp(value, min, max);

            return startValue;
        }
    }
    
    [Serializable]
    public class LockedVector
    {
        [SerializeField] private LockedVariable x, y, z;

        public void SetValues(Vector3 vector)
        {
            x.value = vector.x;
            y.value = vector.y;
            z.value = vector.z;
        }

        public Vector3 GetLockedVector()
        {
            var vector = new Vector3(x.LockVariable(),y.LockVariable(),z.LockVariable());
            return vector;
        }
    }
    
    private Transform _transform;

    private Vector3 _startPosition;

    [SerializeField] private LockedVector lockedPosition;
    [SerializeField] private bool localPosition;
    [SerializeField] private bool fixedUpdate = true;

    private void Start()
    {
        _transform = transform;

        _startPosition = _transform.position;
    }

    private void OnValidate()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (!fixedUpdate)
            Lock();
    }

    private void FixedUpdate()
    {
        if (fixedUpdate)
            Lock();
    }

    private void Lock()
    {
        if (localPosition)
        {
            _transform.localPosition = LockedPosition();
            return;
        }
        
        _transform.position = LockedPosition();
    }

    private Vector3 LockedPosition()
    {
        Vector3 lockedVector = _transform.position;

        lockedPosition.SetValues(lockedVector);

        return lockedPosition.GetLockedVector();
    }
    
}
