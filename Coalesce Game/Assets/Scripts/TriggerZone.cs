using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Coalesce
{
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField]
        private List<TriggerObject> _triggerObjects;
        [SerializeField]
        private UnityEvent _entryEvents;
        [SerializeField]
        private UnityEvent _exitEvents;

        private void OnTriggerEnter(Collider other)
        {
            var trigger = other.transform.GetComponentInParent<Trigger>();
            if (trigger == null || !trigger.HasTrigger(_triggerObjects))
                return;

            _entryEvents.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            var trigger = other.transform.GetComponentInParent<Trigger>();
            if (trigger == null || !trigger.HasTrigger(_triggerObjects))
                return;

            _exitEvents.Invoke();
        }
    }
}
