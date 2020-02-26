using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.SpawnManager
{
    public class SpawnManager : MonoBehaviour
    {
        public List<GameObject> prefabs = new List<GameObject>();

        public Vector3 spawnPoint;

        [SerializeField] private Transform root;

        public GameObject Spawn(int id)
        {
            return root ? Instantiate(prefabs[id], root) : Instantiate(prefabs[id], spawnPoint, Quaternion.identity);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(spawnPoint, 0.1f);
        }
    }
}
