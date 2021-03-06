using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class NannyAI : MonoBehaviour, IEventDispatcher
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

        [SerializeField]
        private AudioSource _sceneWideAudioSource;
        public AudioSource SceneWideAudioSource
            => _sceneWideAudioSource;

        [SerializeField]
        private AudioClip _wompWompClip;
        public AudioClip WompWompClip
            => _wompWompClip;

        private NannyStateBase _state;
        private NannyStateBase[] _states;

        private Animator _animator;
        public Animator Animator => _animator;

        public void Transition(NannyStateBase state)
        {
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
            _animator = GetComponentInChildren<Animator>();

            _states = new NannyStateBase[]
            {
                new NannyIdleState(),
                new NannyChaseState(),
                new NannyPickupMessState(),
                new NannyPickupTodzillaState(),
                new NannyCarryTodzillaState(),
                new NannyGoHaveARestState(),
            };
            foreach (var state in _states)
                state.Start(this);
            Transition(_states[0]);
        }

        private void Update()
            => _state?.OnUpdate();
    }
}
