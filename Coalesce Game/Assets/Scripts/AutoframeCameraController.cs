using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Coalesce
{
    public class AutoframeCameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _cameraBoom;
        [SerializeField]
        private Transform _distantTarget;
        public Transform SetDistantTarget(Transform target)
            => _distantTarget = target;

        private float _cameraSmoothDampVelocity;

        public void ToggleCursor()
        {
            if (Cursor.visible)
                HideCursor();
            else
                ShowCursor();
        }

        public void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            var angle = Mathf.Atan2(
                _distantTarget.position.x - _cameraBoom.position.x,
                _distantTarget.position.z - _cameraBoom.position.z) * Mathf.Rad2Deg;

            var eulers = _cameraBoom.eulerAngles;
            eulers.y = Mathf.SmoothDampAngle(_cameraBoom.eulerAngles.y, angle, ref _cameraSmoothDampVelocity, .5f);
            _cameraBoom.eulerAngles = eulers;
        }
    }
}
