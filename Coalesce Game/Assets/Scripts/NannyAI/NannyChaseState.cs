using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyChaseState : NannyStateBase, IEventSubscriber
    {
        public override void OnEnter()
        {
            EventRouter.Subscribe(EventName.NannyFoundMessyBlock, this);
            EventRouter.Subscribe(EventName.TodzillaCaught, this);
            Nanny.GetComponent<NannyController>().SetNavigationTarget(Nanny.ChaseTarget);
            Nanny.Animator.SetBool("IsWalking", true);
        }

        public override void OnExit()
        {
            EventRouter.Unsubscribe(EventName.NannyFoundMessyBlock, this);
            EventRouter.Unsubscribe(EventName.TodzillaCaught, this);
            Nanny.GetComponent<NannyController>().SetNavigationTarget(null);
            Nanny.Animator.SetBool("IsWalking", false);
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
                case EventName.TodzillaCaught:
                    Nanny.SceneWideAudioSource.PlayOneShot(Nanny.WompWompClip, 1f);
                    Nanny.Transition<NannyPickupTodzillaState>();
                    break;
            }
        }
    }
}
