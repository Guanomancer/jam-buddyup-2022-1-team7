using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Coalesce
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class TodzillaController : MonoBehaviour, IEventSubscriber
    {
        [SerializeField, Tooltip("Max speed in m/s")]
        private float _crawlSpeed = 1f;
        [SerializeField, Tooltip("Zero to max m/s time in seconds")]
        private float _crawlAccelleration = 5f;
        [SerializeField, Tooltip("Time to turn to a new angle")]
        private float _turnSpeedScalar = 1f;
        [SerializeField]
        private Transform _cameraBoom;
        [SerializeField]
        private Transform _model;
        [SerializeField, Range(0.01f, 1f)]
        private float _animatorVelocityWalkThreshold = 0.1f;

        [Header("Audio")]
        [SerializeField]
        private AudioClip _leftFootstep;
        [SerializeField]
        private AudioClip _rightFootstep;

        private Rigidbody _body;
        private Animator _animator;
        private AudioSource _audio;

        private Vector3 _crawlInput = Vector3.zero;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _velocitySmoothDamp = Vector3.zero;

        public void CrawlingInput(InputAction.CallbackContext context)
            => _crawlInput = context.ReadValue<Vector2>().RemapToVector3();

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
            if (_animator == null)
                Debug.LogWarning("No animator found in GameObject or any of the children.", this);
            _audio = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            EventRouter.Subscribe(EventName.TodzillaRightFoot, this);
            EventRouter.Subscribe(EventName.TodzillaLeftFoot, this);
        }

        private void OnDisable()
        {
            EventRouter.Unsubscribe(EventName.TodzillaRightFoot, this);
            EventRouter.Unsubscribe(EventName.TodzillaLeftFoot, this);
        }

        public void OnEvent(EventName eventName, IEventDispatcher dispatcher)
        {
            switch(eventName)
            {
                case EventName.TodzillaRightFoot:
                    _audio.PlayOneShot(_rightFootstep);
                    break;
                case EventName.TodzillaLeftFoot:
                    _audio.PlayOneShot(_leftFootstep);
                    break;
            }
        }

        private void FixedUpdate()
        {
            ApplyMovement();
            ApplyRotation();
            ApplyVelocity();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            _animator.SetBool("IsWalking", _velocity.sqrMagnitude >= _animatorVelocityWalkThreshold);
        }

        private void ApplyRotation()
        {
            if (_crawlInput == Vector3.zero)
                return;

            var rotation = Quaternion.LookRotation(_cameraBoom.forward);
            var tmpRotation = Quaternion.Slerp(_model.rotation, rotation, Time.deltaTime * _turnSpeedScalar);
            var eulers = _model.localEulerAngles;
            eulers.y = tmpRotation.eulerAngles.y;
            _model.localEulerAngles = eulers;
        }

        private void ApplyMovement()
            => _velocity = Vector3.SmoothDamp(_velocity, _crawlInput * _crawlSpeed, ref _velocitySmoothDamp, _crawlAccelleration);

        private void ApplyVelocity()
            => _body.MovePosition(transform.localPosition + _model.TransformDirection(_velocity) * Time.deltaTime);
    }
}
