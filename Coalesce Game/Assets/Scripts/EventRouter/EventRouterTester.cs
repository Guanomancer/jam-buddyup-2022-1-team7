using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.EventRouting
{
    public class EventRouterTester : MonoBehaviour, IEventDispatcher, IEventSubscriber
    {
        private void Start()
        {
            EventRouter.Subscribe<StartEvent>(this);
            EventRouter.Subscribe<EndEvent>(this);
            EventRouter.Dispatch(new StartEvent { ID = 10 });
        }

        private void Update()
        {
            EventRouter.Dispatch(new EndEvent { ID = 15 });
            enabled = false;
        }

        public void OnEvent<T>(T eventData)
            where T : IEventData
        {
            switch(eventData)
            {
                case StartEvent startEventData:
                    Debug.Log("Start: " + startEventData.ID);
                    break;
                case EndEvent endEventData:
                    Debug.Log("End: " + endEventData.ID);
                    break;
            }
        }
    }

    public struct StartEvent : IEventData
    {
        public int ID;
    }

    public struct EndEvent : IEventData
    {
        public int ID;
    }
}
