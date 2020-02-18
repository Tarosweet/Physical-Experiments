using UnityEngine;

namespace Experiment_4_new.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Attractable : MonoBehaviour, IAttractable
    {
        private Rigidbody _rigidbody;

        [SerializeField] private ParticleSystem waterDroplets;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }

        public void EnableParticle()
        {
            waterDroplets.gameObject.SetActive(true);
            waterDroplets.Play();
            
            Invoke(nameof(DisableParticle),5f);
        }

        private void DisableParticle()
        {
           // waterDroplets.gameObject.SetActive(false);
            waterDroplets.Stop();
        }
        
    }
}