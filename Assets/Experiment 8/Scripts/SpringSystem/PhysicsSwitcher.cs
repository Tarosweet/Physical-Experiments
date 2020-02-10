using System;
using UnityEngine;


[RequireComponent(typeof(JointsContainer))]
[RequireComponent(typeof(TransformZeroing))]
public class PhysicsSwitcher : MonoBehaviour
{
    private JointsContainer _jointController;

    private TransformZeroing _transformZeroing;

    private void Start()
    {
        _jointController = GetComponent<JointsContainer>();
        _transformZeroing = GetComponent<TransformZeroing>();
    }
    
    private void OnMouseUp()
    {
        if (_jointController.IsAttached())
        {
            _jointController.SetKinematic(false);
            return;
        }
        
        if (_jointController.IsHaveAttaches())
            return;

        _transformZeroing.Load();

        _jointController.SetKinematic(true);
    }
}
