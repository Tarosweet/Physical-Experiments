using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicalMovingBody))]
public class MovingBodyRotation : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    
    private Transform _transform;
    private PhysicalMovingBody _physicalMovingBody;

    void Start()
    {
        _transform = transform;
        _physicalMovingBody = GetComponent<PhysicalMovingBody>();
    }

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        _transform.Rotate(Vector3.up, _physicalMovingBody.KineticEnergy * speed * Time.deltaTime);
    }
}
