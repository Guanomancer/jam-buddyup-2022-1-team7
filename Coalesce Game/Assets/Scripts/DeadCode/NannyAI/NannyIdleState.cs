using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class NannyIdleState : NannyStateBase
    {
        private int _blocksOnStart;
        private bool _firstChase = true;

        public override void OnEnter()
        {
            _blocksOnStart = BlockManager.Instance._messyBlocks.Count;
        }

        public override void OnUpdate()
        {
            if (BlockManager.Instance._messyBlocks.Count > 0 && _firstChase)
            {
                Nanny.Transition<NannyChaseState>();
                _firstChase = false;
            }

            if (BlockManager.Instance._messyBlocks.Count > _blocksOnStart)
            {
                Nanny.Transition<NannyChaseState>();
            }
        }
    }
}
