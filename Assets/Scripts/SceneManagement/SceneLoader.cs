using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private UnityEvent onLoadScene;
        [SerializeField] private UnityEvent onDestroyScene;
        
        private Scene _currentScene;

        public void LoadScene(int id)
        {
            SceneManager.LoadScene(id, LoadSceneMode.Additive);
            _currentScene = SceneManager.GetSceneByBuildIndex(id);
            onLoadScene?.Invoke();
        }

        public void DestroyCurrentAdditiveScene()
        {
            DestroyScene(_currentScene.buildIndex);
        }
        
        public void DestroyScene(int id)
        {
            SceneManager.UnloadSceneAsync(id);
            onDestroyScene?.Invoke();
        }
    }
}
