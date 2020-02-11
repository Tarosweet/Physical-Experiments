using System;
using System.Collections;
using System.Collections.Generic;
using Experiment_4_new.Scripts;
using UnityEngine;

public class AttractionByWaterMolecules : MonoBehaviour
{
    [SerializeField] private float pullForce = 1f;
    [SerializeField] private float attractTime = 2f;
    [SerializeField] private Vector3 attractionDirection = Vector3.down;

    private HingeJoint _hingeJoint;

    [SerializeField] private float breakForce = 800f;

    private void OnTriggerExit(Collider other)
    {
        IAttractable attractable = other.GetComponent<IAttractable>(); //in ontriggerenter?
        
        if (attractable == null)
            return;

        Attract(attractable.GetRigidbody());
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
