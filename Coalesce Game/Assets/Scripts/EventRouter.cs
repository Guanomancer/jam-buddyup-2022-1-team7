using System;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public static class EventRouter
    {
        private static readonly Dictionary<EventName, List<IEventSubscriber>> _subscribers
            = new Dictionary<EventName, List<IEventSubscriber>>();

        public static void Dispatch(EventName eventName, IEventDispatcher dispatcher)
        {
            if (!_subscribers.ContainsKey(eventName))
                return;

            foreach (var subscriber in _subscribers[eventName])
                subscriber.OnEvent(eventName, dispatcher);
        }

        public static void Subscribe(EventName eventName, IEventSubscriber subscriber)
        {
            if (!_subscribers.ContainsKey(eventName))
                _subscribers.Add(eventName, new List<IEventSubscriber>());

            _subscribers[eventName].Add(subscriber);
        }

        public static void Unsubscribe(EventName eventName, IEventSubscriber subscriber)
        {
            if (!_subscribers.ContainsKey(eventName))
                return;

            _subscribers[eventName].Remove(subscriber);
        }
    }

    public enum EventName
    {
        TodzillaRightFoot,
        TodzillaLeftFoot,
        TodzillaMessy,
        TodzillaCaught,

        NannyFoundMessyBlock,
    }
}
