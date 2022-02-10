using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class TodzillaAnimationEventRouter : MonoBehaviour, IEventDispatcher
    {
        public void LeftFoot()
            => EventRouter.Dispatch(EventName.TodzillaLeftFoot, this);

        public void RightFoot()
            => EventRouter.Dispatch(EventName.TodzillaRightFoot, this);
    }
}
