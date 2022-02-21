using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class PerimeterBlockDetector : MonoBehaviour, IEventDispatcher
    {
        private List<BlockController> _blocksInReach = new List<BlockController>();
        public IReadOnlyList<BlockController> BlocksInReach
            => _blocksInReach;

        public void RemoveBlockFromReachList(BlockController block)
            => _blocksInReach.Remove(block);

        public BlockController GetBlockClosestTo(Vector3 point)
        {
            if (_blocksInReach.Count == 0)
                return null;

            float closestSqrMagnitude = float.MaxValue;
            int blockIndex = 0;
            for(int  i= 0; i < _blocksInReach.Count; i++)
            {
                float sqrMag = (_blocksInReach[i].transform.position - point).sqrMagnitude;
                if(sqrMag <= closestSqrMagnitude)
                {
                    closestSqrMagnitude = sqrMag;
                    blockIndex = i;
                }
            }
            return _blocksInReach[blockIndex];
        }

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
            block = collider.GetComponentInParent<BlockController>();
            return block != null && block.IsMessy();
        }
    }
}
