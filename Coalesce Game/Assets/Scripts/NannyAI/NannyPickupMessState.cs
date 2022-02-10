using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyPickupMessState : NannyStateBase
    {
        public override void OnEnter()
        {
            var controller = Nanny.GetComponent<NannyController>();
            controller.SetNavigationTarget(null);
            controller.PickupBlock(
                Nanny.GetComponent<PerimeterBlockDetector>().BlocksInReach[0],
                () => { Nanny.Transition<NannyChaseState>(); }
                );
        }
    }
}
