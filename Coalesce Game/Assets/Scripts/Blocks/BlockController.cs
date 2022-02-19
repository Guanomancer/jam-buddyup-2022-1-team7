using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Coalesce.EventRouting;

namespace Coalesce
{
    public class BlockController : MonoBehaviour, BatchUpdatable
    {
        [SerializeField]
        private bool _countTowardsScore = true;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        private bool _isMessy;

        private static List<BlockController> _allBlocks = new List<BlockController>();
        private static List<BlockController> _rightBlocks = new List<BlockController>();
        private static List<BlockController> _messyBlocks = new List<BlockController>();
        private static int _totalScoringBlocks = 0;

        public static int TotalScoringBlocks => _totalScoringBlocks;
        public static int MessyBlocks => _messyBlocks.Count;
        public static float DestructionRatio => (float)TotalScoringBlocks / MessyBlocks;

        private void Start()
        {
            if(_countTowardsScore)
            {
                BatchUpdater.RequestBatchUpdatingForScene();
                _totalScoringBlocks++;
                _allBlocks.Add(this);
                _rightBlocks.Add(this);
                BatchUpdater.RegisterForUpdating<BlockController>(this);
            }
        }

        public void BatchStarting()
            => SetOriginalState();

        public void BatchUpdate()
        {
            if(IsMessy())
            {
                BatchUpdater.UnregisterForUpdating<BlockController>(this);
                _messyBlocks.Add(this);
                _rightBlocks.Remove(this);
                EventRouter.Dispatch(new EventTypes.ScoringBlockMessy { });
                if (_rightBlocks.Count == 0)
                    EventRouter.Dispatch(new EventTypes.AllScoringBlocksMessy { });
            }
        }

        private void OnDestroy()
        {
            if (_countTowardsScore)
            {
                _allBlocks.Remove(this);
                (_isMessy ? _messyBlocks : _rightBlocks).Remove(this);
            }
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
            
            UpdateIsMessy();
            AllowNannyToPathThrough();

#if UNITY_EDITOR
            if (_isMessy)
                GetComponentInChildren<MeshRenderer>().material.SetColor("_Highlight_Color", Color.red);
#else
            if (_isMessy)
                GetComponentInChildren<MeshRenderer>().material.SetColor("_Highlight_Color", Color.red);
#endif

            return _isMessy;
        }

        private void AllowNannyToPathThrough()
        {
            GetComponentInChildren<NavMeshObstacle>().enabled = !_isMessy;
        }

        private void UpdateIsMessy()
        {
            _isMessy = ComputeMagnitudeFromOrigin() >= Settings.Game.MessynessMagnitydeThreshold ||
                        ComputeSpinFromOrigin() >= Settings.Game.MessynessSpinThreshold;
        }

        private float ComputeMagnitudeFromOrigin()
            => (transform.position - _originalPosition).magnitude;

        private float ComputeSpinFromOrigin()
            => Quaternion.Angle(transform.rotation, _originalRotation);
    }
}
