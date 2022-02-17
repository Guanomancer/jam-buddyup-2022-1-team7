using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyIdleState : NannyStateBase
    {
        private int _blocksOnStart;
        private bool _firstChase = true;
        private BlockManager _bM = GameObject.Find("Managers").GetComponent<BlockManager>();

        public override void OnEnter()
        {
            _blocksOnStart = _bM._messyBlocks.Count;
        }

        public override void OnUpdate()
        {
            if (_bM._messyBlocks.Count > 0 && _firstChase)
            {
                Nanny.Transition<NannyChaseState>();
                _firstChase = false;
            }
            if (_bM._messyBlocks.Count > _blocksOnStart)
            {
                Nanny.Transition<NannyChaseState>();
            }
        }
    }
}
