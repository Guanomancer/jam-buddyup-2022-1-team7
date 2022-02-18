using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class BlockManager : ManagerBase<BlockManager>, IEventDispatcher
    {
        public List<BlockController> _blocks = new List<BlockController>();
        public List<BlockController> _messyBlocks = new List<BlockController>();
        public List<BlockController> _rightBlocks = new List<BlockController>();
        private GameSettings _gameSettings;
        private bool _hasSetBlockOrigins;
        private DestructometerController _destructometer;
        private int _totalBlocks;
        private float _startTime;
        private float _blocksAtStart;
        private float _totalMessyBlocks;

        public float Destruction
            => (float)_totalMessyBlocks / _blocksAtStart;

        public void RegisterBlock(BlockController block, bool isMessy = false)
        {
            _totalBlocks++;
            _blocks.Add(block);
            _rightBlocks.Add(block);
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

        private void Start()
        {
            _gameSettings = GameManager.Instance.GameSettings;
            _startTime = Time.time;
            _blocksAtStart = _blocks.Count;
        }

        private void Update()
        {
            if (Time.time - _startTime < _gameSettings.MessynessCalculationTimeDelay)
                return;

            if (!_hasSetBlockOrigins)
            {
                for (int i = 0; i < _blocks.Count; i++)
                    _blocks[i].SetOriginalState();
                _hasSetBlockOrigins = true;
            }

            for (int i = 0; i < _rightBlocks.Count; i++)
            {
                var block = _rightBlocks[i];
                if(block.IsMessy())
                {
                    EventRouter.Dispatch(EventName.TodzillaMessy, block as IEventDispatcher);
                    i--;
                    _messyBlocks.Add(block);
                    _rightBlocks.Remove(block);
                    _totalMessyBlocks++;
                    //Debug.Log(_messyBlocks.Count + " blocks messy. Blocks remaning: " + _rightBlocks.Count + ". Total blocks: " + _blocks.Count);
                }
            }

            if (_destructometer == null)
                _destructometer = FindObjectOfType<DestructometerController>();
            else
                _destructometer.Destruction = 1f - Destruction;
        }
    }
}
