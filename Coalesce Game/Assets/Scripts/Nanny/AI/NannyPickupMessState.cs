using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.Nanny
{
    public class NannyPickupMessState : NannyStateBase
    {
        public override void OnEnter()
        {
            Controller.ClearNavigationTarget();
            //Controller.StartCoroutine(controller.PickupBlock(
            //    Nanny.GetComponent<PerimeterBlockDetector>().BlocksInReach[0],
            //    () => { Nanny.Transition<NannyChaseState>(); }
            //    ));
        }
    }
}
