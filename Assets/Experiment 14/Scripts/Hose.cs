using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : MonoBehaviour
{
    [SerializeField] private LineRenderer line;

    [SerializeField] private Nozzle nozzle;

    void Update()
    {
        DrawLine();   
    }

    private void DrawLine()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, nozzle.nozzleTransform.position);
    }
}
