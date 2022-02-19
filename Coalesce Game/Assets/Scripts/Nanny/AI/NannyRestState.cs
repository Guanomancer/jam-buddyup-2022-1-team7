using Coalesce.EventRouting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.Nanny
{
    public class NannyRestState : NannyStateBase, IEventSubscriber
    {
        public override void OnEnter()
        {
            EventRouter.Subscribe<EventTypes.ScoringBlockMessy>(this);
        }

        public override void OnExit()
        {
            EventRouter.Unsubscribe<EventTypes.ScoringBlockMessy>(this);
        }

        public void OnEvent<T>(T eventData)
            where T : IEventData
        {
            switch(eventData)
            {
                case EventTypes.ScoringBlockMessy block:
                    Nanny.Transition<NannyChaseState>();
                    break;
            }
        }
    }
}
