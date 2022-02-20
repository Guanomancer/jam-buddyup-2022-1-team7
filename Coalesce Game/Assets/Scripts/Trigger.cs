using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField]
        private List<TriggerObject> _triggers;
        public bool HasTrigger(TriggerObject trigger)
            => _triggers.Contains(trigger);
        public bool HasTrigger(IReadOnlyList<TriggerObject> triggers)
        {
            for (int i = 0; i < triggers.Count; i++)
                if (_triggers.Contains(triggers[i]))
                    return true;
            return false;
        }
    }
}
