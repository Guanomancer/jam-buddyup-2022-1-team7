using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyVoiceController : MonoBehaviour, IEventSubscriber
    {
        public void OnEvent(EventName eventName, IEventDispatcher dispatcher)
        {
            switch(eventName)
            {
                case EventName.TodzillaMessy:
                    Debug.Log("Nanny says: You've made a mess!");
                    break;
                case EventName.TodzillaCaught:
                    Debug.Log("Nanny says: I've got you now.");
                    break;
            }
        }

        private void Awake()
        {
            EventRouter.Subscribe(EventName.TodzillaMessy, this);
            EventRouter.Subscribe(EventName.TodzillaCaught, this);
        }
    }
}
