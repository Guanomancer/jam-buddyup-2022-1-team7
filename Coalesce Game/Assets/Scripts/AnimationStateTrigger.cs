using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class AnimationStateTrigger : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _size = Vector3.one;

        [SerializeField]
        private string _stateName;

        [SerializeField]
        private Animator _animator;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, _size);
        }

        private void Update()
        {
            var colliders = Physics.OverlapBox(transform.position, _size / 2f, transform.rotation);
            foreach(var collider in colliders)
            {
                if(collider.GetComponentInParent<TodzillaController>() == null)
                    continue;

                _animator.SetBool(_stateName, !_animator.GetBool(_stateName));
                enabled = false;
            }
        }
    }
}
