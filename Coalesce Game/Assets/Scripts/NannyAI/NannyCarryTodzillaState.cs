using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyCarryTodzillaState : NannyStateBase
    {
        public override void OnEnter()
            => Nanny.GetComponent<NannyController>().SetNavigationTarget(Nanny.DropTarget);

        public override void OnUpdate()
        {
            if (Time.time < StateEntryTime + 1f)
                return;

            if (Nanny.GetComponent<NannyController>().DistanceToTarget < 1.5f)
                Nanny.StartCoroutine(Nanny.GetComponent<NannyController>().PutDownTodzilla(Nanny.DropTarget, () => { Nanny.Transition<NannyGoHaveARestState>(); }));
        }
    }
}
