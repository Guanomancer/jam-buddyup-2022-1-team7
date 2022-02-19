using System;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public static class EventRouter
    {
        private static Dictionary<Type, List<IEventSubscriber>> _subscribers = new Dictionary<Type, List<IEventSubscriber>>();

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
