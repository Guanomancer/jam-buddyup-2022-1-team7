using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Coalesce.EventRouting;

namespace Coalesce
{
    public class BlockController : MonoBehaviour, IBlock, BatchUpdatable
    {
        [SerializeField]
        private bool _countTowardsScore = true;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        private bool _isMessy;
        private bool _originHasBeenSet;

        private static List<BlockController> _allBlocks = new List<BlockController>();
        private static List<BlockController> _rightBlocks = new List<BlockController>();
        private static List<BlockController> _messyBlocks = new List<BlockController>();
        private static int _totalScoringBlocks;
        private static float _totalScoringBlocksFloat;
        private static int _totalMessyBlocks;

        public static int TotalScoringBlocks => _totalScoringBlocks;
        public static int MessyBlocks => _totalMessyBlocks;
        public static float DestructionRatio => (float)MessyBlocks / TotalScoringBlocks;

        private void Start()
        {
            if (_countTowardsScore)
            {
                BatchUpdater.RequestBatchUpdatingForScene();
                _totalScoringBlocksFloat++;
                _totalScoringBlocks = Mathf.RoundToInt(_totalScoringBlocksFloat * (1f - Settings.Game.BlockGraceRatio));
                _allBlocks.Add(this);
                _rightBlocks.Add(this);
                BatchUpdater.RegisterForUpdating<BlockController>(this);
            }
            else
                GetComponentInChildren<NavMeshObstacle>().enabled = false;
        }

        public void BatchStarting()
            => SetOriginalState();

        public void BatchUpdate()
        {
            if (IsMessy())
            {
                BatchUpdater.UnregisterForUpdating<BlockController>(this);
                _messyBlocks.Add(this);
                _totalMessyBlocks++;
                _rightBlocks.Remove(this);
                EventRouter.Dispatch(new EventTypes.ScoringBlockMessy
                {
                    TotalBlocks = _totalScoringBlocks,
                    MessyBlocks = _totalMessyBlocks,
                    DestructionRatio = DestructionRatio,
                });
                Debug.Log($"{MessyBlocks} / {TotalScoringBlocks} = {DestructionRatio}");
                if (_messyBlocks.Count == 1)
                {
                    EventRouter.Dispatch(new EventTypes.FirstScoringBlockMessy { });
                    EventRouter.Dispatch(new EventTypes.GameStart { });
                }
                if (DestructionRatio == 1f)
                {
                    EventRouter.Dispatch(new EventTypes.AllScoringBlocksMessy { });
                    EventRouter.Dispatch(new EventTypes.GameEnd { });
                }
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
            _originHasBeenSet = true;
        }

        public bool IsMessy()
        {
            if (!_countTowardsScore || !_originHasBeenSet)
                return false;
            if (_isMessy)
                return true;
            
            UpdateIsMessy();
            AllowNannyToPathThrough();

#if UNITY_EDITOR
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
