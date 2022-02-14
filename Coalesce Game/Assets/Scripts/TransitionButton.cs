using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class TransitionButton : MonoBehaviour
    {
        public void Transition(int sceneIndex)
            => GameManager.Instance.TransitionScene(sceneIndex);
    }
}
