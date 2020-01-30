using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.isKinematic = true;
    }

    public void Click()
    {
        rigidbody.isKinematic = false;
    }
}
