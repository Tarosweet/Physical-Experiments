using System;
using System.Collections;
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

        private Scene _mainScene;

        private void Start()
        {
            _mainScene = SceneManager.GetActiveScene();
        }

        public void LoadScene(int id)
        {
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
            SceneManager.LoadScene(id, LoadSceneMode.Additive);
            _currentScene = SceneManager.GetSceneByBuildIndex(id);
            
            onLoadScene?.Invoke();
        }

        private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            SceneManager.SetActiveScene(scene);
        }

        public void DestroyCurrentAdditiveScene()
        {
            SceneManager.sceneUnloaded += SceneManagerOnSceneUnloaded;
            DestroyScene(_currentScene.buildIndex);
        }

        private void SceneManagerOnSceneUnloaded(Scene scene)
        {
            SceneManager.SetActiveScene(_mainScene);
        }

        public void DestroyScene(int id)
        {
            SceneManager.UnloadSceneAsync(id);
            onDestroyScene?.Invoke();
        }
    }
}
