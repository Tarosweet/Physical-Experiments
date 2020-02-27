using UnityEngine;

namespace Experiment_7.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineRenderConnector : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform firstConnection;
        [SerializeField] private Transform secondConnection;

        [SerializeField] private ElasticPlate elasticPlate;
        [SerializeField] private NearCartChecker nearCartChecker;

        private LineRenderer _lineRenderer;
    
        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }
    
        private void Update()
        {
            DrawLine();
        }

        private void OnMouseDown()
        {
            Click();
            Debug.Log("DSa");
        }

        public void Click()
        {
            elasticPlate.PlayUnbendAnimation();

            Destroy(gameObject);
            
            nearCartChecker.PhysicsSimulation();
        }
    
        private void DrawLine()
        {
            if (!_lineRenderer) 
                return;
            
            _lineRenderer.SetPosition(0, firstConnection.position);
            _lineRenderer.SetPosition(1, secondConnection.position);
        }
    }
}
