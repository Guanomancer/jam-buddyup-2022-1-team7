using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coalesce.EventRouting;

namespace Coalesce.Nanny
{
    public class NannyChaseState : NannyStateBase, IEventSubscriber
    {
        public override void OnEnter()
        {
            EventRouter.Subscribe<EventTypes.NannyFoundMessyBlock>(this);
            EventRouter.Subscribe<EventTypes.NannyCanReachZilla>(this);
            Controller.SetNavigationTarget(Nanny.ChaseTarget);
        }

        public override void OnExit()
        {
            EventRouter.Unsubscribe<EventTypes.NannyFoundMessyBlock>(this);
            EventRouter.Unsubscribe<EventTypes.NannyCanReachZilla>(this);
            Controller.ClearNavigationTarget();
        }

        public void OnEvent<T>(T eventData)
            where T : IEventData
        {
            switch (eventData)
            {
                case EventTypes.NannyFoundMessyBlock block:
                    Nanny.Transition<NannyPickupMessState>();
                    break;
                case EventTypes.NannyCanReachZilla zilla:
                    Nanny.Transition<NannyPickupZillaState>();
                    break;
            }
        }
    }
}
