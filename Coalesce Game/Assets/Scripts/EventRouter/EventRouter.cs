using System;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.EventRouting
{
    public static class EventRouter
    {
        private static Dictionary<Type, List<IEventSubscriber>> _subscribers = new Dictionary<Type, List<IEventSubscriber>>();

        private static HashSet<string> _eventTypesToDisplay = new HashSet<string>();
        private static HashSet<string> _eventTypesToNotDisplay = new HashSet<string>();
        private static bool _displayAllEvents;

        public static void DisplayAllEvents()
            => _displayAllEvents = true;

        public static void DontDisplayAllEvents()
            => _displayAllEvents = false;

        public static void DisplayEventType(string typeName)
        {
            if (!_eventTypesToDisplay.Contains(typeName))
                _eventTypesToDisplay.Add(typeName);
        }

        public static void DontDisplayEventType(string typeName)
        {
            if (!_eventTypesToNotDisplay.Contains(typeName))
                _eventTypesToNotDisplay.Add(typeName);
        }

        public static void Subscribe<T>(IEventSubscriber subscriber)
            where T : EventData
        {
            if (!_subscribers.ContainsKey(typeof(T)))
                _subscribers.Add(typeof(T), new List<IEventSubscriber>());
            
            _subscribers[typeof(T)].Add(subscriber);
        }

        public static void Unsubscribe<T>(IEventSubscriber subscriber)
            where T : EventData
        {
            if (!_subscribers.ContainsKey(typeof(T)))
                return;

            _subscribers[typeof(T)].Remove(subscriber);
        }

        public static void Dispatch<T>(T eventData)
            where T : EventData
        {
#if UNITY_EDITOR
            if ((_displayAllEvents && !_eventTypesToNotDisplay.Contains(typeof(T).Name)) ||
                (!_displayAllEvents && _eventTypesToDisplay.Contains(typeof(T).Name)))
                Debug.Log($"{typeof(T).Name} occured.\n{eventData}");
#endif

            if (!_subscribers.ContainsKey(typeof(T)))
                return;

            foreach (var subscriber in _subscribers[typeof(T)])
                subscriber.OnEvent<T>(eventData);
        }
    }

    public interface IEventSubscriber
    {
        void OnEvent<T>(T eventData)
            where T : EventData;
    }

    public interface IEventDispatcher { }

    public interface EventData { }
}
