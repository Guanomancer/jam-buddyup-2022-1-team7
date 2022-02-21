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
        [SerializeField]
        private AudioClip _mainThemeLoop;

        [SerializeField]
        private float _mainThemeLength;
        [SerializeField]
        private float _mainThemeLoopLength;

        private AudioSource _audioSource;
        private float _enableLoopAt;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.Play();
        }

        private void Update()
        {
            if(_enableLoopAt > 0 && Time.time >= _enableLoopAt)
            {
                _audioSource.Stop();
                _audioSource.clip = _mainThemeLoop;
                _audioSource.loop = true;
                _audioSource.Play();
                _enableLoopAt = Time.time + _mainThemeLoopLength;
            }
        }

        public void PlayMainTheme()
        {
            _audioSource.Stop();
            _audioSource.clip = _mainTheme;
            _audioSource.Play();
            _enableLoopAt = Time.time + _mainThemeLength;
        }
    }
}
