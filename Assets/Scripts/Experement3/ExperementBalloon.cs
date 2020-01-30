using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperementBalloon : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsExperement;
    private bool _isPlay;
    private bool _isEnd;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Play();
        if(Input.GetMouseButtonDown(1))
            Back();
    }

    //TODO заменить на метод интерфейса
    public void Play()
    {
        if (!_isPlay && !_isEnd)
        {
            _isPlay = true;
            StartCoroutine(PlayWithDuration());
        }
    }

    public void Back()
    {
        if (!_isPlay && _isEnd)
        {
            _isPlay = true;
            StartCoroutine(BackWithDuration());
        }
    }

    private IEnumerator PlayWithDuration()
    {
        List<IObjectExperement> objectExperements = GetObjectsExperement();

        foreach (IObjectExperement objectExperement in objectExperements)
        {
            objectExperement.Play();
            while (!objectExperement.IsEnd())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        _isPlay = false;
        _isEnd = true;
    }

    private IEnumerator BackWithDuration()
    {
        List<IObjectExperement> objectExperements = GetObjectsExperement();

        foreach (IObjectExperement objectExperement in objectExperements)
        {
            objectExperement.Back();
            while (objectExperement.IsEnd())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        _isPlay = false;
        _isEnd = false;
    }

    private List<IObjectExperement> GetObjectsExperement()
    {
        List<IObjectExperement> objectExperements = new List<IObjectExperement>();
        
        foreach (GameObject gameObject in _objectsExperement)
        {
            objectExperements.Add(gameObject.GetComponent<IObjectExperement>());
        }

        return objectExperements;
    }
}
