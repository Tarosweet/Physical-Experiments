using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JointsContainer))]
public class FixedJoints : MonoBehaviour
{
    private JointsContainer _jointsContainer;
    
    void Start()
    {
        _jointsContainer = GetComponent<JointsContainer>();
        
        if (_jointsContainer.mount)
        {
            CreateFixedJoint(_jointsContainer.mount.rigidbody);
        }
        else
        {
            Debug.LogWarning("Joint container doesnt have mount");
        }
        
        CreateFixedJoint(_jointsContainer.hook.rigidbody);
    }

    private FixedJoint CreateFixedJoint(Rigidbody connectedBody)
    {
        FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = connectedBody;

        return fixedJoint;
    }
}
