using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class EventRouterTester : MonoBehaviour, IEventDispatcher, IEventSubscriber
    {
        private void Start()
        {
            EventRouter.Subscribe<StartEventData>(this);
            EventRouter.Subscribe<EndEventData>(this);
            EventRouter.Dispatch(new StartEventData { ID = 10 });
        }

        private void Update()
        {
            EventRouter.Dispatch(new EndEventData { ID = 15 });
            enabled = false;
        }

        public void OnEvent<T>(T eventData)
            where T : EventData
        {
            switch(eventData)
            {
                case StartEventData startEventData:
                    Debug.Log("Start: " + startEventData.ID);
                    break;
                case EndEventData endEventData:
                    Debug.Log("End: " + endEventData.ID);
                    break;
            }
        }
    }

    public struct StartEventData : EventData
    {
        public int ID;
    }

    public struct EndEventData : EventData
    {
        public int ID;
    }
}
