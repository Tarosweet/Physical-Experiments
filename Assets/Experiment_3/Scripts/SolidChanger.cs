using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidChanger : MonoBehaviour
{
    [SerializeField] private GameObject _solidPrefab;
    
    public SolidDiffusion Change(Node firstSolid, Node secondSolid, Node toNode, NodeContainer container)
    {
        container.RemoveNode(firstSolid);
        container.RemoveNode(secondSolid);
        GameObject diffusionGameObject = Instantiate(_solidPrefab);
        Node node = diffusionGameObject.GetComponent<Node>();
        node.SetParent(toNode);
        toNode.SetChild(node);
        node.SetRotation(toNode.GetRotation().eulerAngles);
        node.SetPosition(toNode.GetChildPosition() + (node.GetPosition() - node.GetParentPosition()));
        SolidDiffusion solidDiffusion = diffusionGameObject.GetComponent<SolidDiffusion>();
        solidDiffusion.SetBottomColor(firstSolid.GetTransform().GetComponent<MeshRenderer>().material.GetColor("_Color2"));
        solidDiffusion.SetTopColor(secondSolid.GetTransform().GetComponent<MeshRenderer>().material.GetColor("_Color2"));
        solidDiffusion.SetDiffusionValue(0);
        container.AddNode(node);
        Destroy(firstSolid.gameObject);
        Destroy(secondSolid.gameObject);
        return solidDiffusion;
    }
}
