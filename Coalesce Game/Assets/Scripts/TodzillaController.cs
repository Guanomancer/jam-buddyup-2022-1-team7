using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Coalesce
{
    [RequireComponent(typeof(Rigidbody))]
    public class TodzillaController : MonoBehaviour
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

        private Rigidbody _body;

        private Vector3 _crawlInput = Vector3.zero;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _velocitySmoothDamp = Vector3.zero;
        private float _angularVelocity = 0;
        private float _angularSmoothDamp = 0;

        public void CrawlingInput(InputAction.CallbackContext context)
            => _crawlInput = context.ReadValue<Vector2>().RemapToVector3();

        private void Awake()
            => _body = GetComponent<Rigidbody>();

        private void FixedUpdate()
        {
            ApplyMovement();
            ApplyRotation();
            ApplyVelocity();
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
