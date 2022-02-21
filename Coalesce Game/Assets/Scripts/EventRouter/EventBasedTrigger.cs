using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Coalesce.EventRouting
{
    public class EventBasedTrigger : MonoBehaviour, IEventSubscriber
    {
        [SerializeField]
        private string[] _typeNames;

        [SerializeField]
        private UnityEvent _events;

        [SerializeField]
        private bool _advanceToNextScene;

        private void OnEnable()
        {
            foreach (var typeName in _typeNames)
                if (EventTypes.EventTypeFromString(typeName, out Type type))
                    EventRouter.Subscribe(type, this);
        }

        private void OnDisable()
        {
            foreach (var typeName in _typeNames)
                if (EventTypes.EventTypeFromString(typeName, out Type type))
                    EventRouter.Subscribe(type, this);
        }

        public void OnEvent<T>(T eventData) where T : IEventData
        {
            _events.Invoke();
            if (_advanceToNextScene)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
