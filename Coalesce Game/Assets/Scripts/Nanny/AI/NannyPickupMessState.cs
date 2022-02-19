using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coalesce.EventRouting;

namespace Coalesce.Nanny
{
    public class NannyPickupMessState : NannyStateBase, IEventSubscriber
    {
        public override void OnEnter()
        {
            EventRouter.Subscribe<EventTypes.NannyPickedUpMessyBlocks>(this);
            Controller.PickupBlock();
        }

        public override void OnExit()
        {
            EventRouter.Unsubscribe<EventTypes.NannyPickedUpMessyBlocks>(this);
        }

        public void OnEvent<T>(T eventData)
            where T : IEventData
        {
            switch (eventData)
            {
                case EventTypes.NannyPickedUpMessyBlocks zilla:
                    Controller.EndPickupBlock();
                    Nanny.Transition<NannyChaseState>();
                    break;
            }
        }
    }
}
