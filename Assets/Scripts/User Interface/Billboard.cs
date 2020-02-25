using UnityEngine;

namespace User_Interface
{
    public class Billboard : MonoBehaviour
    {
        private Transform _transform;
        private Transform _target;
    
        private void Start()
        {
            _transform = transform;
            _target = Camera.main.transform;
        }
        
        private void Update()
        {
            Facing();
        }

        private void Facing()
        {
            if (!_target)
            {
                Debug.LogWarning("Camera is missing!");
                return;
            }

            var rotation = _target.rotation;
            _transform.LookAt(transform.position + rotation * Vector3.forward, 
                rotation * Vector3.up);
        }
    }
}

