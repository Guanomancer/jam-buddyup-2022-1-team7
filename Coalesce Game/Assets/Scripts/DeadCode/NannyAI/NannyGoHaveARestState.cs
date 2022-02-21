using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class NannyGoHaveARestState : NannyStateBase
    {
        private int _blocksOnStart;

        public override void OnEnter()
        {
            Nanny.GetComponent<NannyController>().SetNavigationTarget(Nanny.RestTarget);
            Nanny.Animator.SetBool("IsWalking", true);
            _blocksOnStart = BlockManager.Instance._messyBlocks.Count;
        }

        public override void OnExit()
        {
            Nanny.Animator.SetBool("IsWalking", false);
        }

        public override void OnUpdate()
        {
            if (Time.time < StateEntryTime + 1f)
                return;

            if (Nanny.GetComponent<NannyController>().DistanceToTarget < 1.5f)
                Nanny.Transition<NannyIdleState>();

            else if (BlockManager.Instance._messyBlocks.Count > _blocksOnStart)
            {
                Nanny.Transition<NannyChaseState>();
            }
        }
    }
}
