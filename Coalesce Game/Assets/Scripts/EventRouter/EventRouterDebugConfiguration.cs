using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.EventRouting
{
    public class EventRouterDebugConfiguration : MonoBehaviour
    {
        [SerializeField]
        private bool _displayAllEventTypes = true;
        [SerializeField]
        private string[] _eventTypesToDisplay;

        private void Awake()
        {
            if (_displayAllEventTypes)
                EventRouter.DisplayAllEvents();

            foreach (var eventType in _eventTypesToDisplay)
                EventRouter.DisplayEventType(eventType);
        }
    }
}
