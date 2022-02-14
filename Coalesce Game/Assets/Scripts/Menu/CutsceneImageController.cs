using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Coalesce
{
    public class CutsceneImageController : MonoBehaviour
    {
        [SerializeField]
        private float _displayTime = 5f;
        [SerializeField]
        private GameObject _nextElement;
        [SerializeField]
        private int _nextScene = -1;

        private float _startTime;

        private void OnEnable()
            => _startTime = Time.time;

        private void Update()
        {
            if (Time.time >= _startTime + _displayTime)
            {
                gameObject.SetActive(false);
                if(_nextElement == null)
                {
                    if (_nextScene == -1)
                        Debug.LogWarning("No next element found after this cutscene element and no next scene is set.", this);
                    else
                        GameManager.Instance.TransitionScene(_nextScene);
                }
                else
                    _nextElement.SetActive(true);
            }
        }
    }
}
