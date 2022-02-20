using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coalesce.DeadCode;
using UnityEngine.SceneManagement;

namespace Coalesce
{
    public class TransitionButton : MonoBehaviour
    {
        public void Transition(int sceneIndex)
            => SceneManager.LoadScene(sceneIndex);
    }
}
