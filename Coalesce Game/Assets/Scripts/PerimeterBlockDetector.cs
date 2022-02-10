using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class PerimeterBlockDetector : MonoBehaviour, IEventDispatcher
    {
        private List<BlockController> _blocksInReach = new List<BlockController>();
        public IReadOnlyList<BlockController> BlocksInReach
            => _blocksInReach;

        public void RemoveBlockFromReachList(BlockController block)
            => _blocksInReach.Remove(block);

        private void OnTriggerEnter(Collider other)
        {
            if (!ColliderToMessyBlock(other, out BlockController block))
                return;

            _blocksInReach.Add(block);
            EventRouter.Dispatch(EventName.NannyFoundMessyBlock, this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!ColliderToMessyBlock(other, out BlockController block))
                return;

            _blocksInReach.Remove(block);
        }

        private bool ColliderToMessyBlock(Collider collider, out BlockController block)
        {
            block = collider.GetComponent<BlockController>();
            return block != null && block.IsMessy();
        }
    }
}
