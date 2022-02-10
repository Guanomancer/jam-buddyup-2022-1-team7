using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Coalesce
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterController))]
    public class NannyController : MonoBehaviour, IEventDispatcher
    {
        [SerializeField]
        private float _reachDistance = 2.5f;
        [SerializeField]
        private float _pickupTimePerBlock = 1f;
        [SerializeField]
        private Transform _todzillaCarryAnchor;

        private Transform _todzillaCarryReference;
        private Transform _navigationTarget;

        private NavMeshAgent _agent;
        private CharacterController _char;

        private NavMeshPath _path;
        private NavMeshPath _currentPath;

        private bool _couldReachTodzilla;

        public void SetNavigationTarget(Transform navigationTarget)
        {
            _navigationTarget = navigationTarget;
            if (_navigationTarget != null)
                _agent.SetDestination(_navigationTarget.position);
        }

        public float DistanceToTarget
            => _agent.remainingDistance;

        public IEnumerator PickupBlock(BlockController block, System.Action andThen)
        {
            yield return new WaitForSeconds(_pickupTimePerBlock);
            GetComponent<PerimeterBlockDetector>().RemoveBlockFromReachList(block);
            Destroy(block.gameObject);
            andThen();
        }

        public IEnumerator PickupTodzilla(Transform todzilla, System.Action andThen)
        {
            _todzillaCarryReference = todzilla;
            _todzillaCarryReference.GetComponent<TodzillaController>().enabled = false;
            _todzillaCarryReference.GetComponent<Rigidbody>().isKinematic = true;
            yield return new WaitForSeconds(_pickupTimePerBlock);
            _todzillaCarryReference.parent = _todzillaCarryAnchor;
            _todzillaCarryReference.localPosition = Vector3.zero;
            _todzillaCarryReference.localRotation = Quaternion.identity;
            andThen();
        }

        public IEnumerator PutDownTodzilla(Transform dropTarget, System.Action andThen)
        {
            yield return new WaitForSeconds(_pickupTimePerBlock);
            _todzillaCarryReference.parent = null;
            _todzillaCarryReference.position = dropTarget.position + Vector3.up * 0.2f;
            _todzillaCarryReference.GetComponent<TodzillaController>().enabled = true;
            _todzillaCarryReference.GetComponent<Rigidbody>().isKinematic = false;
            andThen();
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _char = GetComponent<CharacterController>();
            _path = new NavMeshPath();
        }

        private void FixedUpdate()
        {
            RecomputeRoute();

            if (!_couldReachTodzilla && CanReacyTodzilla)
            {
                _couldReachTodzilla = true;
                EventRouter.Dispatch(EventName.TodzillaCaught, this);
            }
            else if (!CanReacyTodzilla)
                _couldReachTodzilla = false;
        }

        private void RecomputeRoute()
        {
            if (_navigationTarget == null)
            {
                _agent.SetDestination(transform.position);
                return;
            }

            _path.ClearCorners();
            if (_agent.CalculatePath(_navigationTarget.position, _path))
            {
                _currentPath = _path;
                _path = new NavMeshPath();
                _agent.SetPath(_currentPath);
            }
        }

        private bool CanReacyTodzilla
            => _navigationTarget != null &&
                    _navigationTarget.GetComponent<TodzillaController>() != null &&
                    Vector3.Magnitude(_navigationTarget.position - transform.position) <= _reachDistance;
    }
}
