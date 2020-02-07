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

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    public void Connect(JointsContainer hookJointsContainer)
    {
        if (IsAttached())
            return;
        
        if (!IsNotSelf(hookJointsContainer))
            return;

        _hingeJoint = CreateJoint(hookJointsContainer.hook.rigidbody); //TODO кинематик убирать у пружины

        currentHook = hookJointsContainer.hook;
        
        ChainResolver.Resolve(jointsContainer, hookJointsContainer);
    }

    public void Disconnect()
    {
        if (!IsAttached())
            return;
        
        Destroy(_hingeJoint);
        
        if(jointsContainer.IsStaticMount())
            return;

        if (jointsContainer._weightsChain)
        {
            jointsContainer._weightsChain.Remove(jointsContainer);
            
            currentHook.BeDisconnect();
            currentHook = null;
        }
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
