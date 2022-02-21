using Coalesce.EventRouting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.Nanny
{
    public class NannyIdleState : NannyStateBase
    {
        public override void OnEnter()
        {
            Nanny.Transition<NannyChaseState>();
        }
    }
}
