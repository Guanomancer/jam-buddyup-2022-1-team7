using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Coalesce.EventRouting;

namespace Coalesce.Nanny
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NannyController : MonoBehaviour
    {
        [SerializeField]
        private float _reachDistance = 2.5f;
        [SerializeField]
        private float _pickupTimePerBlock = 1f;
        [SerializeField]
        private Transform _zillaCarryAnchor;
        [SerializeField]
        private Transform _zillaCarryCameraFocusAnchor;
        [SerializeField]
        private Transform _zillaCarryReference;
        
        private Transform _navigationTarget;

        private NavMeshAgent _agent;
        private Animator _animator;
        private PerimeterBlockDetector _detector;

        private NavMeshPath _path;
        private NavMeshPath _currentPath;

        private bool _couldReachZilla;

        public void SetNavigationTarget(Transform navigationTarget)
        {
            _navigationTarget = navigationTarget;
            if (_navigationTarget != null)
            {
                _agent.SetDestination(_navigationTarget.position);
                _agent.isStopped = false;
                _animator.SetBool("IsWalking", true);
                EventRouter.Dispatch(new EventTypes.NannyStartedMoving { });
            }
            else
            {
                _agent.isStopped = true;
                _animator.SetBool("IsWalking", false);
                EventRouter.Dispatch(new EventTypes.NannyStoppedMoving { });
            }
        }

        public void ClearNavigationTarget()
            => SetNavigationTarget(null);

        public float DistanceToTarget
            => _agent.remainingDistance;

        public bool CompareTarget(Transform target)
            => _navigationTarget == target;

        public void Pickup()
        {
            _animator.SetBool("Pickup", true);
        }

        public void EndPickup()
        {
            _animator.SetBool("Pickup", false);
        }

        public void PickupBlock()
        {
            _animator.SetBool("PickupBlock", true);
        }

        public void EndPickupBlock()
        {
            _animator.SetBool("PickupBlock", false);
            var block = _detector.GetBlockClosestTo(transform.position);
            var count = 0;
            do
            {
                _detector.RemoveBlockFromReachList(block);
                Destroy(block.gameObject);
                count++;
                block = _detector.GetBlockClosestTo(transform.position);
            } while (block != null && count < Settings.Game.MaxBlocksToPickUpAtOnce);
        }

        public void Putdown()
        {
            _animator.SetBool("Putdown", true);
        }

        public void EndPutdown()
        {
            _animator.SetBool("Putdown", false);
        }

        public void StartCarry()
        {
            _zillaCarryReference.parent = _zillaCarryAnchor;
            _zillaCarryReference.localPosition = Vector3.zero;
            _zillaCarryReference.localRotation = Quaternion.identity;
        }

        public void EndCarry(Transform dropTarget)
        {
            _zillaCarryReference.parent = null;
            _zillaCarryReference.position = dropTarget.position + Vector3.up * 0.2f;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            _detector = GetComponent<PerimeterBlockDetector>();
            _path = new NavMeshPath();
        }

        private void FixedUpdate()
        {
            if (CanReachZilla && !_couldReachZilla)
            {
                _couldReachZilla = true;
                EventRouter.Dispatch(new EventTypes.NannyCanReachZilla { });
            }
            else
                _couldReachZilla = false;
            RecomputeRoute();
        }

        private void RecomputeRoute()
        {
            if (_navigationTarget == null)
                return;

            _path.ClearCorners();
            if (_agent.CalculatePath(_navigationTarget.position, _path))
            {
                _currentPath = _path;
                _path = new NavMeshPath();
                _agent.SetPath(_currentPath);
            }
        }

        private bool CanReachZilla
            => _navigationTarget != null &&
                    _navigationTarget.GetComponent<IZilla>() != null &&
                    Vector3.Magnitude(_navigationTarget.position - transform.position) <= _reachDistance;
    }
}
