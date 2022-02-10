using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
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
        }
    }
}
