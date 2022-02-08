using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class TpsCameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _cameraBoom;

        private void Awake()
        {
            if(_cameraBoom == null)
            {
                Debug.LogWarning($"No camera boom set for {nameof(TpsCameraController)}. Disabling.", this);
                enabled = false;
            }
        }
    }
}
