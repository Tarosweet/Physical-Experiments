using System;
using System.Collections;
using UnityEngine;

namespace Experiment_5.Scripts
{
    public class MoleculeMovement : MonoBehaviour
    {
        [SerializeField] private MoleculeGroup _moleculeGroup;

        [SerializeField] private float speedForward;
        
        private Transform _transform;

        private Coroutine _currentCoroutine;

        private bool _isCanMove;

        private bool _isReverse;

        private void Awake()
        {
            _transform = transform;
        }

        public void MoveTo(Vector3 position, float time)
        {
           _currentCoroutine = StartCoroutine(MoveToPosition(position, time));
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
            _transform.LookAt(direction);
            
            _isCanMove = true;
        }

        private void Update()
        {
            if (_isCanMove)
                MoveForward();
        }

        private void Move(Vector3 startingPoint, Vector3 position, float time)
        {
            if (IsPositionOutOfBox(_transform.position, _moleculeGroup.boundBox))
            {
                position = ReverseDirection(position);
            }

            _transform.position = Vector3.Lerp(_transform.position, position, 
                time);
        }

        private void MoveForward() //TODO разделить по классам движение
        { 
            if (IsPositionOutOfBox(_transform.position, _moleculeGroup.boundBox) && !_isReverse)
            {
                _transform.eulerAngles = ReverseDirection(_transform.rotation.eulerAngles);
                _isReverse = true;
                Debug.Log("Out from box - reversing direction");
            }
            else
            {
                _isReverse = false; //TODO delete?
            }
            
            _transform.position += _transform.forward * speedForward;
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

        private Vector3 ReverseDirection(Vector3 direction)
        {
            return direction * -1;
        }
        
        private bool IsPositionOutOfBox(Vector3 position, Vector3 boxSize)
        {
            if (position.x > boxSize.x / 2 || position.x < -boxSize.x /2) return true;
            if (position.y > boxSize.y / 2 || position.y < -boxSize.y /2) return true;
            if (position.z > boxSize.z / 2|| position.z < -boxSize.z /2) return true;

            return false;
        }
        
    }
}
