using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyGoHaveARestState : NannyStateBase
    {
        public override void OnEnter()
        {
            Nanny.GetComponent<NannyController>().SetNavigationTarget(Nanny.RestTarget);
            Nanny.Animator.SetBool("IsWalking", true);
        }

        public override void OnExit()
        {
            Nanny.Animator.SetBool("IsWalking", false);
        }

        public override void OnUpdate()
        {
            if (Time.time < StateEntryTime + 1f)
                return;

            if (Nanny.GetComponent<NannyController>().DistanceToTarget < 1.5f)
                Nanny.Transition<NannyChaseState>();
        }
    }
}
