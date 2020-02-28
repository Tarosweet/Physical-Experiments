using UnityEngine;

namespace Systems.SpawnManager
{
    public class ReplaceSpawn : SpawnManager
    {
        private GameObject _currentGameObject;
        
        public void SpawnReplace(int id)
        {
            Destroy();
            _currentGameObject = base.SpawnGameObject(id);
        }
        
        protected override void Start()
        {
            if (spawnOnStart)
                SpawnReplace(0);
        }

        private void Destroy()
        {
            if (_currentGameObject)
                Destroy(_currentGameObject);
        }

    }
}
