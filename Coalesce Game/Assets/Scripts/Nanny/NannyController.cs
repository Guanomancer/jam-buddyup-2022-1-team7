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

        private Transform _zillaCarryReference;
        private Transform _navigationTarget;

        private NavMeshAgent _agent;
        private Animator _animator;

        private NavMeshPath _path;
        private NavMeshPath _currentPath;

        private bool _couldReachZilla;

        public void SetNavigationTarget(Transform navigationTarget)
        {
            _navigationTarget = navigationTarget;
            if (_navigationTarget != null)
            {
                _agent.SetDestination(_navigationTarget.position);
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

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
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
                    _navigationTarget.GetComponent<Zilla>() != null &&
                    Vector3.Magnitude(_navigationTarget.position - transform.position) <= _reachDistance;
    }
}
