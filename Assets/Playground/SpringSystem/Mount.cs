using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(SphereCollider))]
public class Mount : MonoBehaviour
{
    public Rigidbody rigidbody;

    private HingeJoint _hingeJoint;

    public JointsContainer jointsContainer;

    private Hook currentHook;

    public BoxCollider collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        collider = GetComponent<BoxCollider>();
    }
    
    public void Connect(JointsContainer hookJointsContainer)
    {

        if (!IsNotSelf(hookJointsContainer))
            return;

        if (jointsContainer.IsInOneChain(hookJointsContainer))
            return;

        transform.position = hookJointsContainer.hook.transform.position;
        
        _hingeJoint = CreateJoint(hookJointsContainer.hook.rigidbody); 

        currentHook = hookJointsContainer.hook;
        
        jointsContainer.SetKinematic(false);
        
        
        ChainResolver.Resolve(jointsContainer, hookJointsContainer);
    }

    public void Disconnect()
    {
        if (!IsAttached())
            return;
        
        Destroy(_hingeJoint);

        if(jointsContainer.IsStaticMount())
            return;
        
        collider.enabled = false;

        currentHook.BeDisconnect();

        if (jointsContainer.IsChainExist())
        {
            jointsContainer.weightsChain.Remove(jointsContainer);
        }
        else
        {
            jointsContainer.GetComponent<InstallationUI>().SetActive(false); //TODO refactor
        }

        var onDisconnect = currentHook.onDisconnectHook;
        currentHook = null;  //TODO refactor
        onDisconnect.Invoke(jointsContainer);
        
    }

    public Rigidbody GetConnectedBody()
    {
        return _hingeJoint.connectedBody;
    }

    public bool IsAttached()
    {
        return _hingeJoint;
    }

    private HingeJoint CreateJoint(Rigidbody connectedBody)
    {
        HingeJoint joint = gameObject.AddComponent<HingeJoint>();

        joint.connectedBody = connectedBody;

        return joint;
    }

    private bool IsNotSelf(JointsContainer container)
    {
        return container != this.jointsContainer;
    }
}
