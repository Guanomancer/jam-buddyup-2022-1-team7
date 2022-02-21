using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Coalesce.DeadCode
{
    public class GameManager : ManagerBase<GameManager>
    {
        [SerializeField]
        private GameSettings _gameSettings;
        public GameSettings GameSettings
            => _gameSettings;

        private SceneController _currentScene;

        private Dictionary<int, SceneController> _scenes = new Dictionary<int, SceneController>();

        private float _sceneStartTime;
        public float SceneTime
            => Time.time - _sceneStartTime;

        public void StartGame()
            => FindObjectOfType<Clock>().StartClock();

        private void Start()
        {
            foreach (var scene in GetComponents<SceneController>())
            {
                if (_scenes.ContainsKey(scene.SceneIndex))
                    Debug.LogError($"A scene with index {scene.SceneIndex} has already been added to the game manager.", this);
                else
                    _scenes.Add(scene.SceneIndex, scene);
            }
            if (!_scenes.ContainsKey(SceneManager.GetActiveScene().buildIndex))
                Debug.LogWarning("Active scene is unknown.", this);
            else
            {
                _currentScene = _scenes[SceneManager.GetActiveScene().buildIndex];
                _currentScene.Enter();
            }
        }

        public void SkipIntro()
        {
            if (_currentScene.SceneIndex == 0)
                TransitionScene(1);
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
            _sceneStartTime = Time.time;
            SceneManager.LoadScene(_currentScene.SceneIndex);
            _currentScene?.Enter();
        }
    }
}
