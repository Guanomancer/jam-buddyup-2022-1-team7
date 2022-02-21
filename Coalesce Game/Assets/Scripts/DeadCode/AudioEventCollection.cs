using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    [CreateAssetMenu(fileName = "New Audio Event Collection", menuName = "Todzilla/DeadCode Audio Event Collection")]
    public class AudioEventCollection : ScriptableObject
    {
        [SerializeField]
        private AudioClip[] _audioClips;
        public IReadOnlyList<AudioClip> AudioClips
            => _audioClips;

        [SerializeField]
        private bool _neverPlayTheSameClipTwice = true;

        private int _lastIndex;

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
                        if (index != _lastIndex || _neverPlayTheSameClipTwice)
                        {
                            _lastIndex = index;
                            return _audioClips[index];
                        }
                    } while (true);
            }
        }
    }
}
