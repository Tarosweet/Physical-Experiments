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
    }
}
