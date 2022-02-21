using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class NannyPickupTodzillaState : NannyStateBase
    {
        public override void OnEnter()
        {
            var controller = Nanny.GetComponent<NannyController>();
            controller.SetNavigationTarget(null);
            controller.StartCoroutine(controller.PickupTodzilla(
                Nanny.ChaseTarget,
                () => { Nanny.Transition<NannyCarryTodzillaState>(); }
                ));
            Nanny.Animator.SetBool("Pickup", true);
        }

        public override void OnExit()
        {
            Nanny.Animator.SetBool("Pickup", false);
        }
    }
}
