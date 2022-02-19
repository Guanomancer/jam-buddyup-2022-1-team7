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
        public static Color ConsoleColor = new Color(0, 0, .8f);

        private static bool _isDispatching;
        private static List<(Type, IEventSubscriber)> _addWaitingList = new List<(Type, IEventSubscriber)>();
        private static List<(Type, IEventSubscriber)> _removeWaitingList = new List<(Type, IEventSubscriber)>();

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
            where T : IEventData
        {
            if (!_subscribers.ContainsKey(typeof(T)))
                _subscribers.Add(typeof(T), new List<IEventSubscriber>());

            if (_isDispatching)
                _addWaitingList.Add((typeof(T), subscriber));
            else
                _subscribers[typeof(T)].Add(subscriber);
        }

        public static void Unsubscribe<T>(IEventSubscriber subscriber)
            where T : IEventData
        {
            if (!_subscribers.ContainsKey(typeof(T)))
                return;

            if (_isDispatching)
                _removeWaitingList.Add((typeof(T), subscriber));
            else
                _subscribers[typeof(T)].Remove(subscriber);
        }

        public static void Dispatch<T>(T eventData)
            where T : IEventData
        {
#if UNITY_EDITOR
            if ((_displayAllEvents && !_eventTypesToNotDisplay.Contains(typeof(T).Name)) ||
                (!_displayAllEvents && _eventTypesToDisplay.Contains(typeof(T).Name)))
                Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>A {3} event occured.</color>\n{4}",
                    (byte)(ConsoleColor.r * 255f),
                    (byte)(ConsoleColor.g * 255f),
                    (byte)(ConsoleColor.b * 255f),
                    typeof(T).Name,
                    eventData));
#endif

            if (!_subscribers.ContainsKey(typeof(T)))
                return;

            _isDispatching = true;
            foreach (var subscriber in _subscribers[typeof(T)])
                subscriber.OnEvent<T>(eventData);
            _isDispatching = false;

            foreach (var subscriber in _addWaitingList)
                _subscribers[subscriber.Item1].Add(subscriber.Item2);
            _addWaitingList.Clear();

            foreach (var subscriber in _removeWaitingList)
                _subscribers[subscriber.Item1].Remove(subscriber.Item2);
            _removeWaitingList.Clear();
        }
    }

    public interface IEventSubscriber
    {
        void OnEvent<T>(T eventData)
            where T : IEventData;
    }

    public interface IEventDispatcher { }

    public interface IEventData { }
}
