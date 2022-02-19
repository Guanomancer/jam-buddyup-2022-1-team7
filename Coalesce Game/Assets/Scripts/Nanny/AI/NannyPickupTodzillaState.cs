using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.Nanny
{
    public class NannyPickupTodzillaState : NannyStateBase
    {
        public override void OnEnter()
        {
            //Controller.StartCoroutine(controller.PickupTodzilla(
            //    Nanny.ChaseTarget,
            //    () => { Nanny.Transition<NannyCarryTodzillaState>(); }
            //    ));
        }
    }
}
