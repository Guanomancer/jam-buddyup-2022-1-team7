using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Coalesce
{
    public class TpsCameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _cameraBoom;
        [SerializeField, Range(10, 80)]
        private float _cameraXClampAngle = 25f;

        private float _rotationOffsetX;
        private float _rotationX;

        private void Awake()
        {
            if (_cameraBoom == null)
            {
                Debug.LogWarning($"No camera boom set for {nameof(TpsCameraController)}. Disabling.", this);
                enabled = false;
                return;
            }

            _rotationOffsetX = _cameraBoom.localEulerAngles.x;
        }

        public void CameraInput(InputAction.CallbackContext context)
        {
            var vector = context.ReadValue<Vector2>();

            _rotationX = Mathf.Clamp(_rotationX + vector.y, -_cameraXClampAngle, _cameraXClampAngle);
            
            var eulers = _cameraBoom.localEulerAngles;
            eulers.x = _rotationOffsetX + _rotationX;
            eulers.y += vector.x;
            _cameraBoom.localEulerAngles = eulers;
        }
    }
}
