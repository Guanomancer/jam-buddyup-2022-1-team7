using Coalesce.EventRouting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.Nanny
{
    public class NannyGoHaveARestState : NannyStateBase
    {
        public override void OnEnter()
        {
            Controller.SetNavigationTarget(Nanny.RestTarget);
            EventRouter.Dispatch(new EventTypes.NannyGoingToRest { });
        }

        public override void OnUpdate()
        {
            if(Controller.CompareTarget(Nanny.RestTarget) &&
                Controller.DistanceToTarget < 1.5f)
                Nanny.Transition<NannyRestState>();
        }
    }
}
