using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class ManagerBase<T> : MonoBehaviour
         where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void Awake()
            => Instance = this as T;
    }
}
