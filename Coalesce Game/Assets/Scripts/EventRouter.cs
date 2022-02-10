using System;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public static class EventRouter
    {
        private static readonly Dictionary<EventName, List<IEventSubscriber>> _subscribers
            = new Dictionary<EventName, List<IEventSubscriber>>();

        private static IEventSubscriber[] _tmpSubs = new IEventSubscriber[100];

        public static void Dispatch(EventName eventName, IEventDispatcher dispatcher)
        {
            if (!_subscribers.ContainsKey(eventName))
                return;

            _subscribers[eventName].CopyTo(_tmpSubs);
            for (int i = 0; i < _subscribers[eventName].Count; i++)
                _tmpSubs[i].OnEvent(eventName, dispatcher);
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
