using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.EventRouting
{
    public class EventRouterDebugConfiguration : MonoBehaviour
    {
        [SerializeField]
        private Color _consoleMessageColor = new Color(0, 0, .8f);
        [SerializeField]
        private bool _displayAllEventTypes = true;
        [SerializeField]
        private string[] _eventTypesToDisplay;
        [SerializeField]
        private string[] _eventTypesToNotDisplay;

        private void Awake()
        {
            if (_displayAllEventTypes)
                EventRouter.DisplayAllEvents();

            foreach (var eventType in _eventTypesToDisplay)
                EventRouter.DisplayEventType(eventType);

            foreach (var eventType in _eventTypesToNotDisplay)
                EventRouter.DontDisplayEventType(eventType);

            EventRouter.ConsoleColor = _consoleMessageColor;
        }
    }
}
