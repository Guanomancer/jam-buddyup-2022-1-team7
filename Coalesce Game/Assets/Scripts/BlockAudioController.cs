using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class BlockAudioController : MonoBehaviour
    {
        private GameSettings _gameSettings;
        private AudioSource _audio;

        private float _lastCollisionMagnutude;
        private float _lastCollisionTime;

        private void Start()
        {
            _gameSettings = GameManager.Instance.GameSettings;
            _audio = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
            => OnCollisionStay(collision);

        private void OnCollisionStay(Collision collision)
        {
            if (_audio == null)
                return;
            var soundConfig = collision.transform.GetComponent<SoundConfiguration>();
            if (soundConfig != null && soundConfig.IgnoreBlockCollisions)
                return;
            var magnitude = collision.relativeVelocity.magnitude;
            if (magnitude < _gameSettings.BlockCollisionSoundTriggerImpulseThreshold)
                return;
            if (_lastCollisionTime + 0.5f > Time.time)
                return;

            _lastCollisionTime = Time.time;
            Debug.Log(collision.transform.name + " F: " + magnitude);
            _audio.PlayOneShot(_gameSettings.BlockCollisionSounds.GetRandom());
        }
    }
}
