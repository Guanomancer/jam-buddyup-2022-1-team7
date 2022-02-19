using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Coalesce
{
    public class BlockController : MonoBehaviour, IEventDispatcher
    {
        [SerializeField]
        private bool _countTowardsScore = true;
        public bool CountTowardsScore => _countTowardsScore;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        private GameSettings _gameSettings;
        private AudioSource _audio;

        private bool _isMessy;

        private void Start()
        {
            _gameSettings = GameManager.Instance.GameSettings;
            _audio = GetComponent<AudioSource>();

            if(_countTowardsScore)
                BlockManager.Instance?.RegisterBlock(this, false);
        }

        private void OnDestroy()
        {
            if(_countTowardsScore)
                BlockManager.Instance.UnregisterBlock(this);
        }

        public void SetOriginalState()
        {
            _originalPosition = transform.position;
            _originalRotation = transform.rotation;
        }

        public bool IsMessy()
        {
            if (_isMessy)
                return true;

            _isMessy = ComputeMagnitudeFromOrigin() >= _gameSettings.MessynessMagnitydeThreshold ||
                        ComputeSpinFromOrigin() >= _gameSettings.MessynessSpinThreshold;

            GetComponentInChildren<NavMeshObstacle>().enabled = !_isMessy;

#if UNITY_EDITOR
            if (_isMessy)
                GetComponentInChildren<MeshRenderer>().material.SetColor("_Highlight_Color", Color.red);
#else
            if (_isMessy)
                GetComponentInChildren<MeshRenderer>().material.SetColor("_Highlight_Color", Color.red);
#endif

            return _isMessy;
        }

        private float ComputeMagnitudeFromOrigin()
            => (transform.position - _originalPosition).magnitude;

        private float ComputeSpinFromOrigin()
            => Quaternion.Angle(transform.rotation, _originalRotation);
    }
}
