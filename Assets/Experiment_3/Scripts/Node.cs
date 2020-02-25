using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, INode
{
    [SerializeField] private Transform _transform;
    [SerializeField] private INodeContainer _nodeContainer;
    [SerializeField] private Vector3 _childPos;
    [SerializeField] private Vector3 _parentPos;
    [SerializeField] private Node _childNode;
    [SerializeField] private Node _parentNode;
    [SerializeField] private float _minDistance = 10;
    [SerializeField] private float _speedMovement = 10;
    [SerializeField] private float _speedRotation = 10;

    private bool _isMoving = false;
    private bool _isTaken = false;
    private Coroutine _moveToParentCoroutine;

    public Action<Node> OnConnectNode;
    public Action OnDisonnectNode;

    private void Start()
    {
        _transform = transform;
    }

    public Transform GetTransform()
    {
        return _transform;
    }
    public Vector3 GetPosition()
    {
        return _transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

    public Quaternion GetRotation()
    {
        return _transform.rotation;
    }

    public void SetRotation(Vector3 rotation)
    {
        _transform.rotation = Quaternion.Euler(rotation);
    }

    public Vector3 GetChildPosition()
    {
        return _transform.position + _transform.right * _childPos.x + _transform.up * _childPos.y + _transform.forward * _childPos.z;
    }

    public Vector3 GetParentPosition()
    {
        return _transform.position + _transform.right * _parentPos.x + _transform.up * _parentPos.y + _transform.forward * _parentPos.z;
    }

    public void SetContainerNodes(INodeContainer container)
    {
        _nodeContainer = container;
    }

    public bool IsMoving()
    {
        return _isMoving;
    }

    public bool IsEmptyChild()
    {
        return _childNode == null;
    }

    public bool IsEmptyParent()
    {
        return _parentNode == null;
    }

    public INode GetChild()
    {
        return _childNode;
    }

    public INode GetParent()
    {
        return _parentNode;
    }

    public void SetChild(INode node)
    {
        _childNode = (Node)node;
    }

    public void SetParent(INode parent)
    {
        _parentNode = (Node)parent;
    }

    private void StopMove()
    {
        if (_moveToParentCoroutine != null)
        {
            StopCoroutine(_moveToParentCoroutine);
            _moveToParentCoroutine = null;
            _isMoving = false;
        }
    }

    public void Disconnect()
    {
        if (_parentNode != null && _childNode != null)
        {
            _childNode.StopMove();
            _parentNode.StopMove();
            _childNode.SetParent(_parentNode);
            _parentNode.SetChild(_childNode);
            _childNode.DisplaceAll();
            SetParent(null);
            SetChild(null);
            return;
        }
        
        if (_parentNode != null)
        {
            _parentNode.StopMove();
            _parentNode.SetChild(null);
            SetParent(null);
        }

        if (_childNode != null)
        {
            _childNode.StopMove();
            _childNode.SetParent(null);
            SetChild(null);
        }
    }

    private void ResetRotation()
    {
        _transform.rotation = Quaternion.identity;
    }

    public void DisplaceAll()
    {
        StopMove();
        StartMove();

        if(_childNode != null)
            _childNode.DisplaceAll();
    }
    private void StartMove()
    {
        _moveToParentCoroutine = StartCoroutine(MoveSmooth());
    }

    private IEnumerator MoveSmooth()
    {
        _isMoving = true;
        float distance = Vector3.Distance(GetParentPosition(), _parentNode.GetChildPosition());
        float angle = Quaternion.Angle(GetRotation(), _parentNode.GetRotation());
        while (distance > 0 || angle > 0 || _parentNode.IsMoving())
        {
            _transform.position =
                Vector3.MoveTowards(GetPosition(), _parentNode.GetChildPosition() + (GetPosition() - GetParentPosition()), Time.deltaTime * _speedMovement);
            distance = Vector3.Distance(GetParentPosition(), _parentNode.GetChildPosition());

            _transform.rotation =
                Quaternion.Lerp(GetRotation(), _parentNode.GetRotation(), Time.deltaTime * _speedRotation); 
            angle = Quaternion.Angle(GetRotation(), _parentNode.GetRotation());
            
            yield return new WaitForSeconds(0.01f);
        }

        _isMoving = false;
    }
    
    private void Take()
    {
        _isTaken = true;
        StopMove();
        Disconnect();
        ResetRotation();
    }

    private INode FindNodeByDistance(float distance)
    {
        List<Node> nodes = _nodeContainer.GetNodes();
        List<INode> nearestNodes = new List<INode>();
        List<float> distances = new List<float>();
        Vector3 thisPos = this.GetPosition();
        foreach (var node in nodes)
        {
            float nodeDistance = Vector3.Distance(thisPos, node.GetPosition());
            if (nodeDistance <= distance && node.IsEmptyChild())
            {
                nearestNodes.Add(node);
                distances.Add(nodeDistance);
            }
        }

        nearestNodes.Remove(this);
        
        if (nearestNodes.Count > 0)
        {
            int finalIndex = 0;
            for (int i = 1; i < nearestNodes.Count; i++)
            {
                if (distances[i] < distances[finalIndex])
                {
                    finalIndex = i;
                }
            }

            return nearestNodes[finalIndex];
        }
        
        return null;
    }

    private bool TryConnect()
    {
        INode node = FindNodeByDistance(_minDistance);
        
        if(node == null)
            return false;
        
        node.SetChild(this);
        this.SetParent(node);
        
        OnConnectNode?.Invoke((Node)node);
        
        return true;
    }

    protected virtual void OnMouseDown()
    {
        OnDisonnectNode?.Invoke();
        Take();
    }
    
    protected virtual void OnMouseUp()
    {
        if (TryConnect())
        {
            StartMove();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.right * _childPos.x + transform.up * _childPos.y + transform.forward * _childPos.z, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + transform.right * _parentPos.x + transform.up * _parentPos.y + transform.forward * _parentPos.z, 0.1f);
    }
}
