using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public interface IEventDispatcher
    {
    }

    public interface IEventSubscriber
    {
        void OnEvent(EventName eventName, IEventDispatcher dispatcher);
    }
}
