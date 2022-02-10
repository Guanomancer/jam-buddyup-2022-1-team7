using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyChaseState : NannyStateBase, IEventSubscriber
    {
        public override void OnEnter()
        {
            Nanny.GetComponent<NannyController>().SetNavigationTarget(Nanny.ChaseTarget);
        }

        public override void OnExit()
        {
            Nanny.GetComponent<NannyController>().SetNavigationTarget(null);
        }

        public override void OnStart()
        {
            EventRouter.Subscribe(EventName.NannyFoundMessyBlock, this);
        }

        public override void OnUpdate()
        {
        }

        public void OnEvent(EventName eventName, IEventDispatcher dispatcher)
        {
            switch (eventName)
            {
                case EventName.NannyFoundMessyBlock:
                    Nanny.Transition<NannyPickupMessState>();
                    break;
            }
        }
    }
}
