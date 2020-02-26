using UnityEngine;

namespace Systems.SpawnManager
{
    public class ReplaceSpawn : SpawnManager
    {
        private GameObject _currentGameObject;
        
        public void SpawnReplace(int id)
        {
            Destroy();
            _currentGameObject = base.Spawn(id);
        }

        public void Destroy()
        {
            if (_currentGameObject)
                Destroy(_currentGameObject);
        }
    }
}
