using Coalesce.EventRouting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.UI
{
    public class ScoreKeeper : MonoBehaviour, IEventSubscriber
    {
        public int TotalBlocks { get; private set; }

        public int MessyBlocks { get; private set; }

        public float DestructionRatio { get; private set; }

        private void Awake()
            => DontDestroyOnLoad(this);

        public void SetScore(int messyBlocks, int totalBlocks, float destructionRatio)
        {
            MessyBlocks = messyBlocks;
            TotalBlocks = totalBlocks;
            DestructionRatio = destructionRatio;
        }

        private void OnEnable()
            => EventRouter.Subscribe<EventTypes.ScoringBlockMessy>(this);
        private void OnDisable()
            => EventRouter.Unsubscribe<EventTypes.ScoringBlockMessy>(this);

        public void OnEvent<T>(T eventData) where T : IEventData
        {
            switch (eventData)
            {
                case EventTypes.ScoringBlockMessy data:
                    SetScore(data.MessyBlocks, data.TotalBlocks, data.DestructionRatio);
                    break;
            }
        }
    }
}
