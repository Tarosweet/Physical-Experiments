using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirInThePipe : MonoBehaviour, IObjectExperiment
{
    [SerializeField] private List<ParticleSystem> _particles;
    private bool _isEnd = false;
    public void Play()
    {
        StartCoroutine(PlayWithDuration(3f));
    }

    private IEnumerator PlayWithDuration(float duration)
    {
        StartParticle();
        yield return new WaitForSeconds(duration);
        StopParticle();
        yield return new WaitForSeconds(duration);
        _isEnd = true;
    }
    private void StartParticle()
    {
        foreach (ParticleSystem particle in _particles)
        {
            particle.Play();
        }
    }
    private void StopParticle()
    {
        foreach (ParticleSystem particle in _particles)
        {
            particle.Stop();
        }
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
