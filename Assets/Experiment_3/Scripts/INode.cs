using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    Vector3 GetPosition();
    void SetPosition(Vector3 position);
    Quaternion GetRotation();
    void SetRotation(Vector3 rotation);
    Vector3 GetChildPosition();
    Vector3 GetParentPosition();
    void SetContainerNodes(INodeContainer container);
    bool IsMoving();
    bool IsEmptyChild();
    bool IsEmptyParent();
    INode GetChild();
    INode GetParent();
    void SetChild(INode node);
    void SetParent(INode parent);
}
