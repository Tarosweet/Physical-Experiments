using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirInThePipe : MonoBehaviour, IObjectExperement
{
    [SerializeField] private List<ParticleSystem> _particles;
    private bool _isEnd = false;
    private bool _isPlay = false;
    
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
        _isPlay = true;
        foreach (ParticleSystem particle in _particles)
        {
            particle.Play();
        }
        _isPlay = false;

    }
    private void StopParticle()
    {
        _isPlay = true;
        
        foreach (ParticleSystem particle in _particles)
        {
            particle.Stop();
        }
        
        _isPlay = false; 

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

    public bool IsPlay()
    {
        return _isPlay;
    }
}
