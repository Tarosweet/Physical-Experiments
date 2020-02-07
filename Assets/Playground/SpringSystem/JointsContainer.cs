using System.Collections;
using System.Collections.Generic;
using Playground.SpringSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JointsContainer : MonoBehaviour, IHavingMass
{
    public Mount mount; //TODO interface GetRigidbody? list<Rb>?
    public Hook hook;

    public Rigidbody rigidbody;

    public WeightsChain _weightsChain;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public float GetMass()
    {
        return rigidbody.mass;
    }

    public bool IsChainExist()
    {
        return _weightsChain;
    }

    public void DisconnectMount()
    {
        mount.Disconnect();
    }

    public bool IsStaticMount()
    {
        return GetComponent<StaticMount>();
    }

    public bool IsAttached()
    {
        return mount.IsAttached();
    }

    public bool IsHaveAttaches()
    {
       return hook.IsAttached();
    }

}
