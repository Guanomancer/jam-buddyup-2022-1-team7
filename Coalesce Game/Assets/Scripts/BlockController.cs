using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class BlockController : MonoBehaviour, IEventDispatcher
    {
        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        private GameSettings _gameSettings;
        private AudioSource _audio;

        private bool _isMessy;

        private void Start()
        {
            _gameSettings = GameManager.Instance.GameSettings;
            _audio = GetComponent<AudioSource>();

            BlockManager.Instance?.RegisterBlock(this, false);
        }

        private void OnDestroy()
            => BlockManager.Instance.UnregisterBlock(this);

        public void SetOriginalState()
        {
            _originalPosition = transform.localPosition;
            _originalRotation = transform.localRotation;
        }

        public bool IsMessy()
        {
            if (_isMessy)
                return true;

            _isMessy = ComputeMagnitudeFromOrigin() >= _gameSettings.MessynessMagnitydeThreshold ||
                        ComputeSpinFromOrigin() >= _gameSettings.MessynessMagnitydeThreshold;
            return _isMessy;
        }

        private float ComputeMagnitudeFromOrigin()
            => (transform.localPosition - _originalPosition).magnitude;

        private float ComputeSpinFromOrigin()
            => Quaternion.Angle(transform.localRotation, _originalRotation);
    }
}
