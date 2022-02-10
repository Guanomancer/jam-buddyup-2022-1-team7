using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyIdleState : NannyStateBase
    {
        public override void OnUpdate()
        {
            if (Time.time >= GameManager.Instance.GameSettings.MessynessCalculationTimeDelay)
                Nanny.Transition<NannyChaseState>();
        }
    }
}
