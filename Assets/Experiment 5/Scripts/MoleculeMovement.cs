using System.Collections;
using UnityEngine;

namespace Experiment_5.Scripts
{
    public class MoleculeMovement : MonoBehaviour
    {
        [SerializeField] private float movementTolerance = Mathf.Epsilon;
        
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        public void MoveTo(Vector3 position, float time)
        {
            StartCoroutine(MoveToPosition(position, time));
        }

        private void Move(Vector3 startingPoint, Vector3 position, float time)
        {
            _transform.position = Vector3.Lerp(startingPoint, position, 
                time);
        }

        private bool IsDestinationPointReached(Vector3 destination, Vector3 currentPosition)
        {
            return Vector3.Distance(destination, currentPosition) < movementTolerance;
        }

        private IEnumerator MoveToPosition(Vector3 destinationPoint, float time)
        {
            var elapsedTime = 0f;
            var startingPos = _transform.position;
            
            while (elapsedTime < time)
            {
                Move(startingPos, destinationPoint, elapsedTime / time);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
