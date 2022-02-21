using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Coalesce.Cameras
{
    public class CameraBoomController : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private float _minDistanceFromZilla = 1.5f;
        [SerializeField]
        private string[] _ignoreLayers;

        private float _distance;

        private void Start()
        {
            _distance = _camera.transform.localPosition.z;
        }

        private void Update()
        {
            var ray = new Ray(transform.position, -_camera.transform.forward);
            var distance = _distance;
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 10f, ~LayerMask.GetMask(_ignoreLayers)))
                distance =
                    Mathf.Min(-_minDistanceFromZilla,
                        Mathf.Max(_distance, -hitInfo.distance)
                    );
            var position = _camera.transform.localPosition;
            position.z = distance;
            _camera.transform.localPosition = position;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
        }
    }
}
