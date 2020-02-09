using System.Collections;
using System.Collections.Generic;
using Playground.SpringSystem;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class JointsContainer : MonoBehaviour, IHavingMass
{
    public Mount mount; //TODO interface GetRigidbody? list<Rb>?
    public Hook hook;

    public Rigidbody rigidbody;

    public WeightsChain weightsChain;

    [SerializeField] private List<Rigidbody> _additionalBodys = new List<Rigidbody>(); //TODO отдельный класс?
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
        if (GetComponent<FixedJoints>())
            _additionalBodys.Add(rigidbody);
    }

    public void SetKinematic(bool value)
    {
        foreach (var additionalBody in _additionalBodys)
        {
            additionalBody.isKinematic = value;
        }
    }

    public float GetMass()
    {
        return rigidbody.mass;
    }

    public bool IsChainExist()
    {
        return weightsChain;
    }

    public void DisconnectMount()
    {
        mount.Disconnect();
        SetKinematic(true);
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

    public bool IsInOneChain(JointsContainer anotherContainer)
    {
        if (IsChainExist())
            return (weightsChain.containers.Contains(anotherContainer));

        return false;
    }

    public Hook GetLastHookSequentially()
    {
        Hook lastHook = hook;
        while (lastHook)
        {
            if (lastHook.currentMount)
            {
                Debug.Log(lastHook.currentMount);
                lastHook = lastHook.currentMount.jointsContainer.hook;
            }
            else
            {
                return lastHook;
            }
        }

        return lastHook;
    }
}
