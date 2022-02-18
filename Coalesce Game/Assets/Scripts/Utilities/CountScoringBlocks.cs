using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class CountScoringBlocks : MonoBehaviour
    {
        [SerializeField]
        private Transform _rootObject;

        [ContextMenu("Count Objects")]
        public void CountObjects()
        {
            var blocks = _rootObject.GetComponentsInChildren<BlockController>();
            var count = 0;

            foreach(var block in blocks)
            {
                if (block.CountTowardsScore)
                    count++;
            }

            Debug.Log($"{count} blocks found in {_rootObject.name} that count towards the player score.", _rootObject);
        }
    }
}
