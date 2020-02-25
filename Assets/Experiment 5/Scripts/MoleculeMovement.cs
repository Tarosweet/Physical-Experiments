using System;
using System.Collections;
using UnityEngine;

namespace Experiment_5.Scripts
{
    public class MoleculeMovement : MonoBehaviour
    {
        public Transform moleculeTransform;

        [SerializeField] private MoleculeGroup _moleculeGroup;

        [Header("Movement to position")]
        [SerializeField] private float timeToReachPoint = 2f;
        
        [Header("Forward movement")]
        [SerializeField] private float speedForward = 0.1f;
        
        private Coroutine _currentCoroutine;

        private bool _isCanMove;

        private bool _isReverse;

        private bool _isOutOfBounds;

        private void Awake()
        {
            moleculeTransform = transform;
        }

        public void MoveTo(Vector3 position, float time)
        {
           _currentCoroutine = StartCoroutine(MoveToPosition(position, time));
           _isOutOfBounds = false;
        }

        public void StopMove()
        {
            if (_currentCoroutine == null)
                return;
            
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
        
        public void StartMoveInRandomDirection(Vector3 direction)
        {
            moleculeTransform.LookAt(direction);
            
            _isCanMove = true;
        }

        private void Update()
        {
            if (_isCanMove)
                MoveForward();
        }

        private void MoveToPosition(Vector3 startingPoint, Vector3 position, float time)
        {
            if (_moleculeGroup.IsOutOfBounds(moleculeTransform.position) && !_isOutOfBounds)
            {
                position = Vector3.zero;
                StopMove();
                MoveTo(position, time);
                _isOutOfBounds = true; //TODO REFACTOR
                return;
            }

            moleculeTransform.position = Vector3.Lerp(moleculeTransform.position, position, 
                time);
        }

        private void MoveForward() //TODO разделить по классам движение
        { 
          /*  if (_moleculeGroup.IsOutOfBounds(moleculeTransform.position) && !_isReverse)
            {
                moleculeTransform.eulerAngles = ReverseDirection(moleculeTransform.rotation.eulerAngles);
                _isReverse = true;
                Debug.Log("Out from box - reversing direction");
            }
            else
            {
                if (!_moleculeGroup.IsOutOfBounds(moleculeTransform.position))
                 _isReverse = false; //TODO delete?
            } */
            
            moleculeTransform.position += moleculeTransform.forward * speedForward;
        }

        private void OnCollisionEnter(Collision other1)
        {
            moleculeTransform.eulerAngles = ReverseDirection(moleculeTransform.rotation.eulerAngles);
        }

        private IEnumerator MoveToPosition(Vector3 destinationPoint, float time)
        {
            var elapsedTime = 0f;
            var startingPos = moleculeTransform.position;
            
            while (elapsedTime < time)
            {
                MoveToPosition(startingPos, destinationPoint, elapsedTime / time);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        private Vector3 ReverseDirection(Vector3 direction)
        {
            return direction * -1;
        }
    }
}
