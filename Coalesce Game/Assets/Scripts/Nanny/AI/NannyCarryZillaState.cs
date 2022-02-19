using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.Nanny
{
    public class NannyCarryZillaState : NannyStateBase
    {
        public override void OnEnter()
        {
            Controller.SetNavigationTarget(Nanny.DropTarget);
        }

        public override void OnExit()
        {
            Controller.ClearNavigationTarget();
        }

        public override void OnUpdate()
        {
            if (Controller.CompareTarget(Nanny.DropTarget) &&
                Controller.DistanceToTarget < 1.5f)
                Nanny.Transition<NannyPutDownZillaState>();
        }
    }
}
