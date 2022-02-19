using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coalesce.EventRouting;

namespace Coalesce.Nanny
{
    public class NannyAnimationEventRouter : MonoBehaviour
    {
        public void PickupEnd()
            => EventRouter.Dispatch(new EventTypes.NannyPickedUpZilla { });

        public void PutdownEnd()
            => EventRouter.Dispatch(new EventTypes.NannyPutDownZilla { });
    }
}
