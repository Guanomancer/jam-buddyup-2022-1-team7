using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "Todzilla/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Physics")]
        [SerializeField, Tooltip("Units (meters) of movement a block can experience, before it counts as being a mess.")]
        private float _messynessMagnitudeThreshold = .5f;
        public float MessynessMagnitydeThreshold
            => _messynessMagnitudeThreshold;
        [SerializeField, Tooltip("Degrees of spin a block can experience, before it counts as being a mess.")]
        private float _messynessSpinThreshold = 30f;
        public float MessynessSpinThreshold
            => _messynessSpinThreshold;
        [SerializeField, Tooltip("Time in seconds before messyness calculations start. This will let the blocks settle before calculating which ones have moved.")]
        private float _messynessCalculationTimeDelay = 5f;
        public float MessynessCalculationTimeDelay
            => _messynessCalculationTimeDelay;

        [Header("Sounds")]
        [SerializeField]
        private AudioEventCollection _blockCollisionSounds;
        public AudioEventCollection BlockCollisionSounds
            => _blockCollisionSounds;
        [SerializeField]
        private float _blockCollisionSoundTriggerImpulseThreshold = 3f;
        public float BlockCollisionSoundTriggerImpulseThreshold
            => _blockCollisionSoundTriggerImpulseThreshold;

    }
}
