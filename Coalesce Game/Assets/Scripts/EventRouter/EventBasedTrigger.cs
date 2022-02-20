using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Coalesce.EventRouting
{
    public class EventBasedTrigger : MonoBehaviour, IEventSubscriber
    {
        [SerializeField]
        private string[] _typeNames;

        [SerializeField]
        private UnityEvent _events;

        private void OnEnable()
        {
            foreach (var typeName in _typeNames)
                if (TypeFromString(typeName, out Type type))
                    EventRouter.Subscribe(type, this);
        }

        private void OnDisable()
        {
            foreach (var typeName in _typeNames)
                if (TypeFromString(typeName, out Type type))
                    EventRouter.Subscribe(type, this);
        }

        private bool TypeFromString(string typeName, out Type type)
        {
            type = typeof(EventTypes).GetNestedType(typeName);
            return type != null;
        }

        public void OnEvent<T>(T eventData) where T : IEventData
        {
            _events.Invoke();
        }
    }
}
