using Coalesce.EventRouting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.Nanny
{
    public class NannyIdleState : NannyStateBase, IEventSubscriber
    {
        public override void OnEnter()
        {
            EventRouter.Subscribe<EventTypes.FirstScoringBlockMessy>(this);
        }

        public override void OnExit()
        {
            EventRouter.Unsubscribe<EventTypes.FirstScoringBlockMessy>(this);
        }

        public void OnEvent<T>(T eventData)
            where T : IEventData
        {
            switch (eventData)
            {
                case EventTypes.FirstScoringBlockMessy block:
                    Nanny.Transition<NannyChaseState>();
                    break;
            }
        }
    }
}
