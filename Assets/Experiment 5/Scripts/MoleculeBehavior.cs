using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Experiment_5.Scripts
{
    public abstract class MoleculeBehavior : MonoBehaviour
    {
        [Header("Time")]
        [SerializeField] private float minimalSecondsToChangeDirection = 0.5f;
        [SerializeField] private float maximalSecondsToChangeDirection = 2f;
        
        [Header("Vector Multiplier")]
        [SerializeField] protected float minDirectionVectorMultiplier = -10f;
        [SerializeField] protected float maxDirectionVectorMultiplier = 10f;
        
        [Header("Molecule Group")]
        [SerializeField] protected MoleculeGroup moleculeGroup;
        
        private float _secondsToChangeDirection;
        
        protected abstract void MoveInNewDirection();

        private void Start()
        {
            _secondsToChangeDirection = SetRandomTimer();

            MoveInNewDirection();
        }

        private void Update()
        {
            if (!IsTimeHasCome(ref _secondsToChangeDirection)) 
                return;
            
            _secondsToChangeDirection =
                Random.Range(minimalSecondsToChangeDirection, maximalSecondsToChangeDirection);
            
            MoveInNewDirection();
        }

        private float SetRandomTimer()
        {
            return Random.Range(minimalSecondsToChangeDirection, maximalSecondsToChangeDirection);
        }

        private bool IsTimeHasCome(ref float time)
        {
            if (!(time > 0))
                return false;
            
            time -= Time.deltaTime;
            return time < Mathf.Epsilon;
        }
    }
}
