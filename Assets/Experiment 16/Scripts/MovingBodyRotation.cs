using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

[RequireComponent(typeof(PhysicalMovingBody))]
public class MovingBodyRotation : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float speedWithContolls = 50f;

    [SerializeField] private float distanceToRotate = 0.3f;
    
    private Transform _transform;
    private PhysicalMovingBody _physicalMovingBody;

    private Vector3 _lastPosition;

    void Start()
    {
        _transform = transform;
        _physicalMovingBody = GetComponent<PhysicalMovingBody>();

        _lastPosition = _transform.position;
    }

    void Update()
    {
        Rotate(_physicalMovingBody.KineticEnergy * speed);
    }

    public void ChangeDirection()
    {
        speed *= (-1f);
    }

    public void RotateWithPlayerContolls()
    {
        if (!VectorExtension.IsPassedDistanceInDirection(_transform.position, _lastPosition,
            VectorExtension.Axis.Y, distanceToRotate))
            return;
        
        switch (GetDirection(_transform.position, _lastPosition))
        {
            case VectorExtension.Direction.Up:
                Rotate(speedWithContolls);
                break;
            case VectorExtension.Direction.Down:
                Rotate(-speedWithContolls);
                break;
        }

        _lastPosition = _transform.position;
    }

    private VectorExtension.Direction GetDirection(Vector3 currentPosition, Vector3 lastPosition)
        {
            return VectorExtension.GetVectorDirection(
                VectorExtension.DirectionVectorNormalized(currentPosition, lastPosition));
        }

        private void Rotate(float rotationSpeed)
        {
            _transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
}
