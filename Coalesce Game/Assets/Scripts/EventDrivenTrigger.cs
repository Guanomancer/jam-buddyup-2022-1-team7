using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Coalesce
{
    public class EventDrivenTrigger : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _events;
        [SerializeField]
        private bool _once;

        private bool _hasBeenTriggered;

        public void Trigger()
        {
            if (_once && _hasBeenTriggered)
                return;
            _hasBeenTriggered = true;
            _events.Invoke();
        }
    }
}
