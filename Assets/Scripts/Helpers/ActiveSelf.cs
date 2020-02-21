using System;
using UnityEngine;


public class ActiveSelf : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private void OnValidate()
    {
        if (_gameObject == null)
            _gameObject = gameObject;
    }

    public void ReverseActive()
    {
        _gameObject.SetActive(!_gameObject.activeSelf);
    }
}