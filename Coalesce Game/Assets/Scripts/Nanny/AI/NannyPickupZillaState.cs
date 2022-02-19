using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coalesce.EventRouting;

namespace Coalesce.Nanny
{
    public class NannyPickupZillaState : NannyStateBase, IEventSubscriber
    {
        public override void OnEnter()
        {
            EventRouter.Subscribe<EventTypes.NannyPickedUpZilla>(this);
            Controller.Pickup();
        }

        public override void OnExit()
        {
            EventRouter.Unsubscribe<EventTypes.NannyPickedUpZilla>(this);
        }

        public void OnEvent<T>(T eventData)
            where T : IEventData
        {
            switch(eventData)
            {
                case EventTypes.NannyPickedUpZilla zilla:
                    Controller.EndPickup();
                    Controller.StartCarry();
                    Nanny.Transition<NannyCarryZillaState>();
                    break;
            }
        }
    }
}
