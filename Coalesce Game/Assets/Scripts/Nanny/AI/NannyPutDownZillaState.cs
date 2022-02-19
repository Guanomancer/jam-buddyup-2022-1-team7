using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coalesce.EventRouting;

namespace Coalesce.Nanny
{
    public class NannyPutDownZillaState : NannyStateBase, IEventSubscriber
    {
        public override void OnEnter()
        {
            EventRouter.Subscribe<EventTypes.NannyPutDownZilla>(this);
            Controller.Putdown();
        }

        public override void OnExit()
        {
            EventRouter.Unsubscribe<EventTypes.NannyPutDownZilla>(this);
        }

        public void OnEvent<T>(T eventData)
            where T : IEventData
        {
            switch(eventData)
            {
                case EventTypes.NannyPutDownZilla zilla:
                    Controller.EndPutdown();
                    Controller.EndCarry(Nanny.DropTarget);
                    Nanny.Transition<NannyGoHaveARestState>();
                    break;
            }
        }
    }
}
