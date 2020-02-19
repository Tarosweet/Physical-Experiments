using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovingBodyRotation))]
public class PhysicalMovingBody : MonoBehaviour
{
    [SerializeField] private Vector3 threadEndPoint;

    [SerializeField] private float energyToTurn = 0.7f;

    [SerializeField] private float timeToTurn = 3f;

    [HideInInspector] public Rigidbody _rigidbody;
    
    public float PotentialEnergy => RigidbodyExtension.PotentialEnergy(_rigidbody.mass, Height);
    public float KineticEnergy => RigidbodyExtension.KineticEnergy(_rigidbody);

    private Transform _transform;

    private MovingBodyRotation _movingBodyRotation;
    
    private bool _isCanTurn;

    public float Height => _transform.position.y - threadEndPoint.y;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _movingBodyRotation = GetComponent<MovingBodyRotation>();
    }

    private void Update()
    {
        if (IsPotentialEnergyLess(energyToTurn))
        {
            Turn(_rigidbody);
        }

        if (IsKineticEnergyLess(energyToTurn))
        {
            Turn();
        }
    }

    private void Turn(Rigidbody body)
    {
        if (!_isCanTurn)
            return;

        var velocity = body.velocity;
        var flippedVelocity = new Vector3(velocity.x, velocity.magnitude, velocity.z);

        //body.velocity = flippedVelocity;
         StartCoroutine(LerpVelocity(flippedVelocity, timeToTurn)); //Looks good but not right
        
         Debug.Log("Turn");
        _movingBodyRotation.ChangeDirection();

        _isCanTurn = false;
    }

    private void Turn()
    {
        _isCanTurn = true;
    }

    private IEnumerator LerpVelocity(Vector3 velocity ,float time)
    {
        float elapsedTime = 0;
        var startingVector = _rigidbody.velocity;

        while (elapsedTime < time)
        {
            _rigidbody.velocity = Vector3.Lerp(startingVector, velocity, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private bool IsPotentialEnergyLess(float value)
    {
        return PotentialEnergy < value;
    }

    private bool IsKineticEnergyLess(float value)
    {
        return KineticEnergy < value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(threadEndPoint, Vector3.one);
    }
}
