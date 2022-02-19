using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class ThemePlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _corridorTheme;
        [SerializeField]
        private AudioClip _mainTheme;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.Play();
        }

        public void PlayMainTheme()
        {
            _audioSource.Stop();
            _audioSource.clip = _mainTheme;
            _audioSource.Play();
        }
    }
}
