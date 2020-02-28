using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.SpawnManager
{
    public class SpawnManager : MonoBehaviour
    {
        public List<GameObject> prefabs = new List<GameObject>();

        public Vector3 spawnPoint;

        [SerializeField] protected bool spawnOnStart;

        [SerializeField] private Transform root;
        
        public void Spawn(int id)
        {
            SpawnGameObject(id);
        }
        
        protected GameObject SpawnGameObject(int id)
        {
            return root ? Instantiate(prefabs[id], root) : Instantiate(prefabs[id], spawnPoint, Quaternion.identity);
        }
        
        protected virtual void Start()
        {
            if (spawnOnStart)
                Spawn(0);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(spawnPoint, 0.1f);
        }
    }
}
