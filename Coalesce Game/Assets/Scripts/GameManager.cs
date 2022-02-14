using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Coalesce
{
    public class GameManager : ManagerBase<GameManager>
    {
        [SerializeField]
        private GameSettings _gameSettings;
        public GameSettings GameSettings
            => _gameSettings;

        private SceneController _currentScene;

        private Dictionary<int, SceneController> _scenes = new Dictionary<int, SceneController>();

        private void Start()
        {
            foreach (var scene in GetComponents<SceneController>())
            {
                if (_scenes.ContainsKey(scene.SceneIndex))
                    Debug.LogError($"A scene with index {scene.SceneIndex} has already been added to the game manager.", this);
                else
                    _scenes.Add(scene.SceneIndex, scene);
            }
            _currentScene = _scenes[SceneManager.GetActiveScene().buildIndex];
            _currentScene.Enter();
        }

        public void TransitionScene(int sceneIndex)
        {
            if(!_scenes.ContainsKey(sceneIndex))
            {
                Debug.LogError($"Scene {sceneIndex} does not exist in the game manager.", this);
                return;
            }
            var scene = _scenes[sceneIndex];
            _currentScene?.Exit();
            _currentScene = scene;
            SceneManager.LoadScene(_currentScene.SceneIndex);
            _currentScene?.Enter();
        }
    }
}
