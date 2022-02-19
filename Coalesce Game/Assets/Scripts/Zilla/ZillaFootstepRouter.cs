using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coalesce.EventRouting;

namespace Coalesce.Zilla
{
    public class ZillaFootstepRouter : MonoBehaviour
    {
        public void LeftFoot()
       => EventRouter.Dispatch(new ZillaLeftFoot { });

        public void RightFoot()
            => EventRouter.Dispatch(new ZillaRightFoot { });
    }
}
