using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringEffectParticle : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private ParticleSystem _particleSystem;

    public float GetDelay()
    {
        return _particleSystem.main.duration;
    }
    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

    public void SetSize(float size)
    {
        var particleSystemMain = _particleSystem.main;
        particleSystemMain.startSize = size;
    }

    public void SetColor(Color color)
    {
        var particleSystemMain = _particleSystem.main;
        particleSystemMain.startColor = color;
    }

    public void SetColliderTrigger(Collider trigger)
    {
        _particleSystem.trigger.SetCollider(0,trigger);
    }

    public void SetGravity(float gravity)
    {
        var particleSystemMain = _particleSystem.main;
        particleSystemMain.gravityModifier = gravity;
    }

    public void Stop()
    {
     StartCoroutine(StopPouringDelay());
    }

    IEnumerator StopPouringDelay()
    {
        _particleSystem.Stop();
        yield return new WaitForSeconds(GetDelay());
        _particleSystem.Clear();
        Destroy(this.gameObject);
    }
}
