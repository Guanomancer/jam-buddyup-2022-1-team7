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

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _char = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            if (_navigationTarget != null)
                _agent.SetDestination(_navigationTarget.position);
        }
    }
}
