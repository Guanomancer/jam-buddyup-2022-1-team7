using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class SoundConfiguration : MonoBehaviour
    {
        [SerializeField]
        private bool _ignoreBlockCollisions;
        public bool IgnoreBlockCollisions
            => _ignoreBlockCollisions;
    }
}
