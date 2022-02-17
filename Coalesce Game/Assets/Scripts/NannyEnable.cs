using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyEnable : MonoBehaviour
    {
        public GameObject _nanny, _doorCloser;
        private BlockManager _bM;

        private void Start()
        {
            _bM = GameObject.Find("Managers").GetComponent<BlockManager>();
            _doorCloser.SetActive(false);
        }
        private void Update()
        {
            if (_bM._messyBlocks.Count != 0)
            {
                _nanny.SetActive(true);
                _doorCloser.SetActive(true);
            }
        }
    }
}