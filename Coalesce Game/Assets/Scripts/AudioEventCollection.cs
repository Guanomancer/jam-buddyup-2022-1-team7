using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    [CreateAssetMenu(fileName = "New Audio Event Collection", menuName = "Todzilla/Audio Event Collection")]
    public class AudioEventCollection : ScriptableObject
    {
        [SerializeField]
        private AudioClip[] _audioClips;
        public IReadOnlyList<AudioClip> AudioClips
            => _audioClips;

        [SerializeField]
        private bool _neverPlayTheSameClipTwice = true;
        [SerializeField]
        private bool _skipFirstEvent = false;
        [SerializeField]
        private float _firstEventDelay = -1f;
        [SerializeField]
        private float _intervalDelay = -1f;
        
        [System.NonSerialized]
        private int _lastIndex;
        [System.NonSerialized]
        private bool _isFirst = true;
        [System.NonSerialized]
        private float _disableUntilTime;

        public AudioClip GetRandom()
        {
            switch(_audioClips.Length)
            {
                case 0: return null;
                case 1: return _audioClips[0];
                default:
                    do
                    {
                        var index = Random.Range(0, _audioClips.Length);
                        if (!_neverPlayTheSameClipTwice || index != _lastIndex)
                        {
                            _lastIndex = index;
                            return _audioClips[index];
                        }
                    } while (true);
            }
        }

        public bool ComplexPlay(AudioSource audioSource)
        {
            if (_isFirst && _skipFirstEvent)
            {
                _isFirst = false;
                if (_firstEventDelay != -1)
                    _disableUntilTime = Time.time + _firstEventDelay;
                return false;
            }
            if (Time.time < _disableUntilTime)
                return false;

            var clip = GetRandom();
            Debug.Log($"Nanny says '<color='#00ff00'>{clip.name}</color>' from clip collection {name}");
            audioSource.PlayOneShot(clip);

            if(_intervalDelay != -1)
                _disableUntilTime = Time.time + _intervalDelay;

            return true;
        }
    }
}
