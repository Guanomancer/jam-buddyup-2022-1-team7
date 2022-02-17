using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyEnable : MonoBehaviour
    {
        public GameObject _nanny, _doorCloser;
        public UIManager _ui;
        private int _messyBlocksAtStart;

        private void Start()
        {
            _doorCloser.SetActive(false);
            _messyBlocksAtStart = BlockManager.Instance._messyBlocks.Count;
        }
        private void Update()
        {
            if (BlockManager.Instance._messyBlocks.Count != _messyBlocksAtStart &&
                !_nanny.activeSelf)
            {
                _nanny.SetActive(true);
                _doorCloser.SetActive(true);
                _ui.ActivateUI();
            }
        }
    }
}