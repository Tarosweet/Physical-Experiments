using UnityEngine;


public class ActiveSelf : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    public void ReverseActive()
    {
        _gameObject.SetActive(!_gameObject.activeSelf);
    }
}