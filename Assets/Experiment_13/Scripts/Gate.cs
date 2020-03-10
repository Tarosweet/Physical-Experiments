using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private Transform _gateTransform;

    [SerializeField] private Vector3 _openedDoorPosition;
    [SerializeField] private Vector3 _closedDoorPosition;

    [SerializeField] private Vector3 _openedGatePosition;
    [SerializeField] private Vector3 _closedGatePosition;
    
    [SerializeField] private float _speedDoor;
    [SerializeField] private float _speedGate;
    
    public Action doorOpened;
    public Action doorClosed;

    public Action gateOpened;
    public Action gateClosed;

    public void OpenDoor()
    {
        StartCoroutine(MoveSmooth(_doorTransform, _openedDoorPosition, _speedDoor, doorOpened));
    }

    public void CloseDoor()
    {
        StartCoroutine(MoveSmooth(_doorTransform, _closedDoorPosition, _speedDoor, doorClosed));
    }

    public void OpenGate()
    {
        StartCoroutine(MoveSmooth(_gateTransform, _openedGatePosition, _speedGate, gateOpened));
    }

    public void CloseGate()
    {
        StartCoroutine(MoveSmooth(_gateTransform, _closedGatePosition, _speedGate, gateClosed));
    }

    private IEnumerator MoveSmooth(Transform transform, Vector3 toPos, float speed, Action end)
    {

        float distance = Vector3.Distance(transform.position, toPos);

        while (distance > 0)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, toPos, speed * Time.deltaTime);
            distance = Vector3.Distance(transform.localPosition, toPos);
            yield return new WaitForSeconds(0.01f);
        }

        transform.localPosition = toPos;
        end?.Invoke();
    }
}
