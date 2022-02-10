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
        private Transform _navigationTarget;
        [SerializeField]
        private float _reachDistance = 1.5f;

        private NavMeshAgent _agent;
        private CharacterController _char;

        private NavMeshPath _path;
        private NavMeshPath _currentPath;

        private bool _couldReachTodzilla;

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
                return;

            _path.ClearCorners();
            if (_agent.CalculatePath(_navigationTarget.position, _path))
            {
                _currentPath = _path;
                _path = new NavMeshPath();
                _agent.SetPath(_currentPath);
            }
        }

        private bool CanReacyTodzilla
            => _navigationTarget.GetComponent<TodzillaController>() != null &&
                    Vector3.Magnitude(_navigationTarget.position - transform.position) <= _reachDistance;
    }
}
