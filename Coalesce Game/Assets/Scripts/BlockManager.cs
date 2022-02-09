using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class BlockManager : SingletonBehaviour<BlockManager>
    {
        private List<BlockController> _blocks = new List<BlockController>();
        private List<BlockController> _messyBlocks = new List<BlockController>();
        private List<BlockController> _rightBlocks = new List<BlockController>();

        public void RegisterBlock(BlockController block, bool isMessy = false)
        {
            _blocks.Add(block);
            (isMessy || block.IsMessy() ? _messyBlocks : _rightBlocks).Add(block);
        }

        public void UnregisterBlock(BlockController block)
        {
            _blocks.Remove(block);
            _messyBlocks.Remove(block);
            _rightBlocks.Remove(block);
        }

        public void ClearBlocks()
        {
            _blocks.Clear();
            _messyBlocks.Clear();
            _rightBlocks.Clear();
        }

        private void Update()
        {
            for(int i = 0; i < _rightBlocks.Count; i++)
            {
                var block = _rightBlocks[i];
                if(block.IsMessy())
                {
                    Debug.Log("You've made a mess!");
                    i--;
                    _messyBlocks.Add(block);
                    _rightBlocks.Remove(block);
                }
            }
        }
    }
}
