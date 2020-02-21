using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderBetweenTargets : MonoBehaviour
{
    [SerializeField] private Transform[] targets;

    [SerializeField] private LineRenderer line;

    private Vector3[] _positions;

    private void Start()
    {
        InitializePositions();
    }

    private void OnValidate()
    {
        InitializePositions();
        UpdateLines();
    }

    void Update()
    {
        UpdateLines();
    }

    private void InitializePositions()
    {
        _positions = new Vector3[targets.Length];
        
        for (var i = 0; i < targets.Length; i++)
        {
            _positions[i] = targets[i].position;
        }
    }

    private void UpdateLines()
    {
        InitializePositions();
        line.SetPositions(_positions);
    }
}
