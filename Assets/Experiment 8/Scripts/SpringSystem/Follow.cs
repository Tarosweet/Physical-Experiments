using System;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position = target.position + offset;
    }
}
