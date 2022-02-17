using UnityEngine;
using UnityEngine.Events;

namespace Coalesce
{
    public class AnimationStateTriggerZone : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _size = Vector3.one;

        [SerializeField]
        private string _stateName;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private TriggerUser _triggerUser;

        [SerializeField]
        private UnityEvent _additionalEvents;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, _size);
        }

        private void Update()
        {
            var colliders = Physics.OverlapBox(transform.position, _size / 2f, Quaternion.identity);
            foreach(var collider in colliders)
            {
                if (IsTriggerUser(collider))
                    continue;

                _additionalEvents.Invoke();
                _animator.SetBool(_stateName, !_animator.GetBool(_stateName));
                enabled = false;
            }
        }

        private bool IsTriggerUser(Collider collider)
            => _triggerUser switch
            {
                TriggerUser.Zilla => collider.GetComponentInParent<TodzillaController>() == null,
                TriggerUser.Nanny => collider.GetComponentInParent<NannyController>() == null,
                _ => (collider.GetComponentInParent<TodzillaController>() == null ||
                        collider.GetComponentInParent<NannyController>() == null),
            };
    }

    public enum TriggerUser
    {
        All,
        Zilla,
        Nanny,
    }
}
