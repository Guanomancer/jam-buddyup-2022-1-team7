using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyAI : MonoBehaviour, IEventDispatcher
    {
        [SerializeField]
        private Transform _chaseTarget;
        public Transform ChaseTarget
            => _chaseTarget;

        [SerializeField]
        private string _stateName;

        private NannyStateBase _state;
        private NannyStateBase[] _states;

        public void Transition(NannyStateBase state)
        {
            _state?.OnExit();
            _state = state;
            _stateName = _state.GetType().Name;
            _state?.OnEnter();
        }

        public void Transition<T>()
            where T : NannyStateBase
        {
            foreach (var state in _states)
            {
                if (state is T)
                {
                    Transition(state);
                    return;
                }
            }

            Debug.LogError($"Invalid state {typeof(T).Name}.", this);
        }

        private void Start()
        {
            _states = new NannyStateBase[]
            {
                new NannyIdleState(),
                new NannyChaseState(),
                new NannyPickupMessState(),
            };
            foreach (var state in _states)
                state.Start(this);
            Transition(_states[0]);
        }

        private void Update()
            => _state?.OnUpdate();
    }
}
