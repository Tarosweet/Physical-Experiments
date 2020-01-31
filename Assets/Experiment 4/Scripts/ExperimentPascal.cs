using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentPascal : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> _objectsExperement;
    private bool _isPlay;
    private bool _isEnd;

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
        List<IObjectExperiment> objectExperements = GetObjectsExperiment();

        objectExperements[1].Play();
        objectExperements[2].Play();

        while (!objectExperements[1].IsEnd())
        {
            yield return new WaitForSeconds(0.1f);
        }
        objectExperements[0].Back();
        objectExperements[2].Back();

        _isPlay = false;
        _isEnd = true;
    }

    private IEnumerator BackWithDuration()
    {
        List<IObjectExperiment> objectExperements = GetObjectsExperiment();

        foreach (IObjectExperiment objectExperement in objectExperements)
        {
            objectExperement.Back();
            while (objectExperement.IsEnd())
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        objectExperements[0].Play();

        _isPlay = false;
        _isEnd = false;
    }

    private List<IObjectExperiment> GetObjectsExperiment()
    {
        List<IObjectExperiment> objectExperements = new List<IObjectExperiment>();
        
        foreach (GameObject gameObject in _objectsExperement)
        {
            objectExperements.Add(gameObject.GetComponent<IObjectExperiment>());
        }

        return objectExperements;
    }
    public void Click()
    {
        Play();
    }
}
