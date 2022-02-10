using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Coalesce
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterController))]
    public class NannyController : MonoBehaviour
    {
        [SerializeField]
        private Transform _navigationTarget;
        private NavMeshAgent _agent;
        private CharacterController _char;

        private NavMeshPath _path;
        private NavMeshPath _currentPath;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _char = GetComponent<CharacterController>();
            _path = new NavMeshPath();
        }

        private void FixedUpdate()
        {
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
    }
}
