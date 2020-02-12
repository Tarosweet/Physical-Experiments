using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Es.WaveformProvider;
using UnityEngine;

public class ParticleWaveInput : MonoBehaviour
{
    [SerializeField]
        private Texture2D waveform;

        [SerializeField, Range(0f, 1f)]
        private float inputScaleFitter = 0.01f;

        [SerializeField, Range(0f, 1f)]
        private float strength = 1f;

        private ParticleSystem _particleSystem;
        
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void OnParticleTrigger()
        {
            WaveInput();
        }

        private void WaveInput()
        {
            var numEnter = _particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);


            foreach (var collision in enter)
            {

                var colliders = Physics.OverlapSphere(collision.position, 5);

                var canvas = FindCanvas(colliders);

                if (canvas == null) 
                    return;
   
                Debug.Log("trig");
                canvas.Input(waveform,collision.position , 1 * 1 * inputScaleFitter, strength);   
            }
        }

        private WaveConductor FindCanvas(Collider[] colliders)
        {
            return (from collider in colliders let canvas = collider.GetComponent<WaveConductor>() where collider select canvas).FirstOrDefault();
        }
    }
