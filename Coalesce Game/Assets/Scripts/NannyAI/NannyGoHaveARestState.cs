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
