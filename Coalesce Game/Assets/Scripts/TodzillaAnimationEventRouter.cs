using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class TodzillaAnimationEventRouter : MonoBehaviour, IEventDispatcher
    {
        public void FootstepLeftEvent()
            => EventRouter.Dispatch(EventName.TodzillaLeftFoot, this);

        public void FootstepRightEvent()
            => EventRouter.Dispatch(EventName.TodzillaRightFoot, this);
    }
}
