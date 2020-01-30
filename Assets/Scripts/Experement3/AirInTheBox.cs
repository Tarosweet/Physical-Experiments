using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirInTheBox : MonoBehaviour, IObjectExperement
{
    [SerializeField] private ParticleSystem _particle;
    private bool _isEnd = false;
    private bool _isPlay = false;
    
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
        _isPlay = true;
        _particle.Play();
        _isPlay = false;
        
    }

    private void StopParticle()
    {
        _isPlay = true;
        _particle.Stop();
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
