using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
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
        [SerializeField]
        private float _blockCollisionSoundTriggerVelocityThreshold = 0.5f;
        public float BlockCollisionSoundTriggerVelocityThreshold
            => _blockCollisionSoundTriggerVelocityThreshold;
        [SerializeField]
        private float _blockCollisionSoundTriggerAngularVelocityThreshold = 0.5f;
        public float BlockCollisionSoundTriggerAngularVelocityThreshold
            => _blockCollisionSoundTriggerAngularVelocityThreshold;

        [SerializeField]
        private AnimationCurve _blockCollisionSoundCurve;
        public AnimationCurve BlockCollisionSoundCurve
            => _blockCollisionSoundCurve;

        [Header("Nanny")]
        [SerializeField]
        private float _messyBlockDetectionRadius = 2f;
        public float MessyBlockDetectionRadius
            => _messyBlockDetectionRadius;
        [SerializeField]
        private int _maxBlocksToPickUpAtOnce = 5;
        public int MaxBlocksToPickUpAtOnce
            => _maxBlocksToPickUpAtOnce;
    }
}
