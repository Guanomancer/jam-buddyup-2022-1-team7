using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class SpecificTargetCameraZone : MonoBehaviour
    {
        [SerializeField]
        private Transform _cameraTarget;
        [SerializeField]
        private Vector3 _size;

        private List<Collider> _colliders = new List<Collider>();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, _size);
        }

        private void Update()
        {
            var colliders = Physics.OverlapBox(transform.position, _size / 2f, transform.rotation);
            foreach (var collider in colliders)
            {
                var autoFramer = collider.transform.GetComponentInParent<AutoframeCameraController>();
                if (autoFramer == null ||
                    _colliders.Contains(collider))
                    continue;

                _colliders.Add(collider);
                autoFramer.SetDistantTarget(_cameraTarget);
            }

            for(int c = 0; c < _colliders.Count; c++)
            {
                var collider = _colliders[c];
                bool found = false;
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (collider == colliders[i])
                    {
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    c--;
                    _colliders.Remove(collider);
                    var autoFramer = collider.transform.GetComponentInParent<AutoframeCameraController>();
                    if(autoFramer.CurrentDistantTarget == _cameraTarget)
                        autoFramer.ResetDistantTarget();
                }
            }
        }
    }
}
