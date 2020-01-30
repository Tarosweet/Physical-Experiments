using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderConnector : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform firstConnection;
    [SerializeField] private Transform secondConnection;

    private LineRenderer lineRenderer;

    private ElasticPlate elasticPlate; 
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        elasticPlate = FindObjectOfType<ElasticPlate>();
    }
    
    void Update()
    {
        DrawLine();
    }

    public void Click()
    {
        elasticPlate.PlayUnbendAnimation();

        Destroy(gameObject);
    }
    
    private void DrawLine()
    {
        if (lineRenderer)
        {
            lineRenderer.SetPosition(0, firstConnection.position);
            lineRenderer.SetPosition(1, secondConnection.position);
        }
    }
}
