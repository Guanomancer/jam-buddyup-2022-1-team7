using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class BlockAudioController : MonoBehaviour
    {
        private GameSettings _gameSettings;
        private AudioSource _audio;
        private Rigidbody _body;

        private float _lastCollisionMagnutude;
        private float _lastCollisionTime;

        private void Start()
        {
            _gameSettings = GameManager.Instance.GameSettings;
            _audio = GetComponent<AudioSource>();
            _body = GetComponent<Rigidbody>();
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
            var velocity = _body.velocity.magnitude;
            var angularVelocity = _body.angularVelocity.magnitude;
            //if (magnitude < _gameSettings.BlockCollisionSoundTriggerImpulseThreshold &&
            //    velocity < _gameSettings.BlockCollisionSoundTriggerVelocityThreshold &&
            //    angularVelocity < _gameSettings.BlockCollisionSoundTriggerAngularVelocityThreshold)
            //    return;
            if (velocity < _gameSettings.BlockCollisionSoundTriggerVelocityThreshold)
                return;
            if (_lastCollisionTime + 0.3f > Time.time)
                return;

            //Debug.Log(collision.transform.name + " Mag: " + magnitude + " Vel: " + velocity + " Ang: " + angularVelocity);
            var volumeScalar = Mathf.Clamp(
                _gameSettings.BlockCollisionSoundCurve.Evaluate(
                    velocity - _gameSettings.BlockCollisionSoundTriggerVelocityThreshold),
                0f, 1f);
            _lastCollisionTime = Time.time;
            _audio.PlayOneShot(_gameSettings.BlockCollisionSounds.GetRandom(), volumeScalar);
        }
    }
}
