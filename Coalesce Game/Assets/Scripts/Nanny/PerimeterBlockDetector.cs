using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coalesce.EventRouting;

namespace Coalesce.Nanny
{
    public class PerimeterBlockDetector : MonoBehaviour
    {
        private List<IBlock> _blocksInReach = new List<IBlock>();
        public IReadOnlyList<IBlock> BlocksInReach
            => _blocksInReach;

        public void RemoveBlockFromReachList(IBlock block)
            => _blocksInReach.Remove(block);

        public IBlock GetBlockClosestTo(Vector3 point)
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
            if (!ColliderToMessyBlock(other, out IBlock block))
                return;

            _blocksInReach.Add(block);
            EventRouter.Dispatch(new EventTypes.NannyFoundMessyBlock { });
        }

        private void OnTriggerExit(Collider other)
        {
            if (!ColliderToMessyBlock(other, out IBlock block))
                return;

            _blocksInReach.Remove(block);
        }

        private bool ColliderToMessyBlock(Collider collider, out IBlock block)
        {
            block = collider.GetComponentInParent<IBlock>();
            return block != null && block.IsMessy();
        }
    }
}
