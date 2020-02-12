using System;
using System.Collections;
using System.Collections.Generic;
using Experiment_4_new.Scripts;
using UnityEngine;

public class AttractionByWaterMolecules : MonoBehaviour
{
    [SerializeField] private Vector3 attractionDirection = Vector3.down;

    private HingeJoint _hingeJoint;

    [SerializeField] private float breakForce = 800f;

    private void OnTriggerExit(Collider other)
    {
        Attractable attractable = other.GetComponent<Attractable>();
        
        if (attractable == null)
            return;

        Attract(attractable.GetRigidbody());
        attractable.EnableParticle();
    }

    private void Attract(Rigidbody attractable)
    {
        _hingeJoint = CreateHingeJoint(attractable);
    }

    private HingeJoint CreateHingeJoint(Rigidbody connectedBody)
    {
        var createdHingeJoint = gameObject.AddComponent<HingeJoint>();

        createdHingeJoint.connectedBody = connectedBody;
        createdHingeJoint.breakForce = breakForce;

        return createdHingeJoint;
    }
}
