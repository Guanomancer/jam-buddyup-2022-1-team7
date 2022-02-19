using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coalesce.EventRouting;

namespace Coalesce.Nanny
{
    public class NannyAI : MonoBehaviour
    {
        [SerializeField]
        private Transform _chaseTarget;
        public Transform ChaseTarget
            => _chaseTarget;

        [SerializeField]
        private Transform _dropTarget;
        public Transform DropTarget
            => _dropTarget;

        [SerializeField]
        private Transform _restTarget;
        public Transform RestTarget
            => _restTarget;

        [SerializeField]
        private string _stateName;

        private NannyStateBase _state;
        private NannyStateBase[] _states;

        private NannyController _controller;

        public void Transition(NannyStateBase state)
        {
            EventRouter.Dispatch(new EventTypes.NannyStateChanged { NewState = state.GetType() });
            _state?.OnExit();
            _state = state;
            _stateName = _state?.GetType().Name;
            _state?.Enter();
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
            _controller = GetComponent<NannyController>();
            _states = new NannyStateBase[]
            {
                new NannyIdleState(),
                new NannyChaseState(),
                new NannyPickupMessState(),
                new NannyPickupZillaState(),
                new NannyCarryZillaState(),
                new NannyPutDownZillaState(),
                new NannyGoHaveARestState(),
            };
            foreach (var state in _states)
                state.Start(this, _controller);
            Transition(_states[0]);
        }

        private void Update()
            => _state?.OnUpdate();
    }
}
