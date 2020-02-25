using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeContainer : MonoBehaviour, INodeContainer
{
    [SerializeField] private List<Node> _nodes;

    private void Start()
    {
        InitializeNodes();
    }

    private void InitializeNodes()
    {
        foreach (var node in _nodes)
        {
            node.SetContainerNodes(this);
        }
    }

    public List<Node> GetNodes()
    {
        return _nodes;
    }

    public void AddNode(Node node)
    {
        _nodes.Add(node);
        node.SetContainerNodes(this);
    }

    public void RemoveNode(Node node)
    {
        node.Disconnect();
        _nodes.Remove(node);
    }
}
