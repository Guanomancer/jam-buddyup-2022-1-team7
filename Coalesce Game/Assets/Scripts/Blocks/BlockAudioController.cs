using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class BlockAudioController : MonoBehaviour
    {
        private AudioSource _audio;
        private Rigidbody _body;

        private float _lastCollisionTime;

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            _body = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
            => OnCollisionStay(collision);

        private void OnCollisionStay(Collision collision)
        {
            if (_audio == null)
                return;
            var soundConfig = collision.transform.GetComponent<AudioConfiguration>();
            if (soundConfig != null && soundConfig.IgnoreBlockCollisions)
                return;

            var magnitude = collision.relativeVelocity.magnitude;
            var angularVelocity = _body.angularVelocity.magnitude;

            if (magnitude < Settings.Game.BlockCollisionSoundTriggerVelocityThreshold)
                return;
            if (_lastCollisionTime + 0.3f > Time.time)
                return;

            //Debug.Log(System.Math.Round(magnitude, 2));
            var volumeScalar = Mathf.Clamp(
                Settings.Game.BlockCollisionSoundCurve.Evaluate(
                    magnitude - Settings.Game.BlockCollisionSoundTriggerVelocityThreshold),
                0f, 1f);
            _lastCollisionTime = Time.time;
            _audio.PlayOneShot(Settings.Game.BlockCollisionSounds.GetRandom(), volumeScalar);
        }
    }
}
