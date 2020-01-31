using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillingSubstance : MonoBehaviour, IObjectExperiment
{
    [SerializeField] private ParticleSystem _particle;
    private bool _isEnd = false;

    public void Play()
    {
        StartParticle();
        _isEnd = true;
    }

    private void StartParticle()
    {
        _particle.Play();
    }

    private void StopParticle()
    {
        _particle.Stop();
    }

    public void Back()
    {
        StopParticle();
        _isEnd = false;
    }

    public bool IsEnd()
    {
        return _isEnd;
    }
}
