using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirInTheBox : MonoBehaviour, IObjectExperiment
{
    [SerializeField] private ParticleSystem _particle;
    private bool _isEnd = false;

    public void Play()
    {
        StartCoroutine(PlayWithDuration(2f));
    }
    
    private IEnumerator PlayWithDuration(float duration)
    {
        StartParticle();
        yield return new WaitForSeconds(duration);
        StopParticle();
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
