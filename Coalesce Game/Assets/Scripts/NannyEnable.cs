using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyEnable : MonoBehaviour
    {
        public GameObject _nanny, _doorCloser;
        private BlockManager _bM;
        public UIManager _ui;

        private void Start()
        {
            _bM = GameObject.Find("Managers").GetComponent<BlockManager>();
            _doorCloser.SetActive(false);
        }
        private void Update()
        {
            if (_bM._messyBlocks.Count != 0 &&
                !_nanny.activeSelf)
            {
                _nanny.SetActive(true);
                _doorCloser.SetActive(true);
                _ui.ActivateUI();
            }
        }
    }
}