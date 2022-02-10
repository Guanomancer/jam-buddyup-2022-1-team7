using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class BlockController : MonoBehaviour
    {
        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        private GameSettings _gameSettings;

        private void Start()
        {
            _gameSettings = GameManager.Instance.GameSettings;

            BlockManager.Instance?.RegisterBlock(this, false);
        }

        public void SetOriginalState()
        {
            _originalPosition = transform.localPosition;
            _originalRotation = transform.localRotation;
        }

        public bool IsMessy()
            => ComputeMagnitudeFromOrigin() >= _gameSettings.MessynessMagnitydeThreshold ||
                ComputeSpinFromOrigin() >= _gameSettings.MessynessMagnitydeThreshold;

        private float ComputeMagnitudeFromOrigin()
            => (transform.localPosition - _originalPosition).magnitude;

        private float ComputeSpinFromOrigin()
            => Quaternion.Angle(transform.localRotation, _originalRotation);
    }
}
