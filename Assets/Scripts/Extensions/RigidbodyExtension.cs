using UnityEngine;

namespace Extensions
{
    public static class RigidbodyExtension
    {
        private static float newtongks = 0.102f;

        public static float ConvertToNewton(float kg)
        {
            return Mathf.Floor(kg / newtongks);
        }

        public static float ConvertToKgs(float newtons)
        {
            return newtons * newtongks;
        }

        public static float PotentialEnergy(float mass, float height)
        {
            return -Physics.gravity.y * mass * height;
        }
        
        public static float KineticEnergy(float mass, float velocityMagnitude)
        {
            return mass * Mathf.Pow(velocityMagnitude, 2) * 0.5f;
        }

        public static float KineticEnergy(Rigidbody body)
        {
            return body.mass * Mathf.Pow(body.velocity.magnitude, 2) * 0.5f;
        }
    }
}
