using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Coalesce
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField]
        private int _sceneIndex = -1;
        public int SceneIndex
            => _sceneIndex;

        public void Enter()
            => OnEnter();

        public void Exit()
            => OnExit();

        protected virtual void OnEnter() { }

        protected virtual void OnExit() { }
    }
}
