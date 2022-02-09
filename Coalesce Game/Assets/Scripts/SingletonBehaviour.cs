using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class SingletonBehaviour<T> : MonoBehaviour
         where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void OnEnable()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}
