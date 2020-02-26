using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearCartChecker : MonoBehaviour
{
    [Serializable]
    private struct CartPhysicsSimulation
    {
        public Rigidbody cartRb;
        public Vector3 force;
    }

    [SerializeField] private CartPhysicsSimulation[] carts;

    [SerializeField] private float maxDistance;

    public void PhysicsSimulation()
    {
        if (IsCartNear())
            AddAllCartsForce();
    }

    private bool IsCartNear()
    {
        if ((!carts[0].cartRb || !carts[1].cartRb))
            return false;
        
        return Vector3.Distance(carts[0].cartRb.position, carts[1].cartRb.position) > maxDistance;
    }

    private void AddForce(Rigidbody rb, Vector3 force)
    {
        rb.velocity = force;
    }

    private void AddAllCartsForce()
    {
        AddForce(carts[0].cartRb, carts[0].force);
        AddForce(carts[1].cartRb, carts[1].force);
    }
}
