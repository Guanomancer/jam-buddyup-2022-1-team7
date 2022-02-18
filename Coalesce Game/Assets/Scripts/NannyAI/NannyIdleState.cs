using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class NannyIdleState : NannyStateBase
    {
        private int _blocksOnStart;
        private bool _firstChase = true;

        [SerializeField]
        private float _nannyWaitTime = 5;

        public override void OnEnter()
        {
            _blocksOnStart = BlockManager.Instance._messyBlocks.Count;
        }

        public override void OnUpdate()
        {
            if (BlockManager.Instance._messyBlocks.Count > 0 && _firstChase)
            {
                var controller = Nanny.GetComponent<NannyController>();
                controller.StartCoroutine(controller.NannyFirstChase(_nannyWaitTime));
                _firstChase = false;
            }

            if (BlockManager.Instance._messyBlocks.Count > _blocksOnStart)
            {
                Nanny.Transition<NannyChaseState>();
            }
        }
    }
}
