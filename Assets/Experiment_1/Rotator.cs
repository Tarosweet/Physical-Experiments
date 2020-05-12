using UnityEngine;

namespace Experiment_1
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Transform rotatingTransform;
        [SerializeField] private float angle = 20f;

        private void OnMouseDrag()
        {
            Debug.Log("reo");
            var x = Input.GetAxis ("Mouse X") * angle * Mathf.Deg2Rad;
        
            rotatingTransform.Rotate(Vector3.forward, x * Time.deltaTime);
        }
    }
}
