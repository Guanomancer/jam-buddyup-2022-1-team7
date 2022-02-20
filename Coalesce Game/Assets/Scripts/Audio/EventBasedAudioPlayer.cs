using Coalesce.EventRouting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.Audio
{
    public class EventBasedAudioPlayer : MonoBehaviour, IEventSubscriber
    {
        [SerializeField]
        private float _delayBetweenOneShots = 1f;
        [SerializeField]
        private List<AudioEventCollection> _eventClips;

        private AudioSource _audio;
        [System.NonSerialized]
        private float _nextOneShotAllowed;

        private void OnEnable()
        {
            _audio = GetComponent<AudioSource>();
            foreach (var type in EventTypes.GetAll())
                EventRouter.Subscribe(type, this);
        }

        private void OnDisable()
        {
            foreach (var type in EventTypes.GetAll())
                EventRouter.Unsubscribe(type, this);
        }

        public void OnEvent<T>(T eventData) where T : IEventData
        {
            switch (eventData)
            {
                default:
                    var name = eventData.GetType().Name;
                    foreach (var clip in _eventClips)
                    {
                        if (clip.name == name)
                        {
                            if (!clip.IgnoreOneShotDelay && Time.time < _nextOneShotAllowed)
                                continue;
                            if (clip.ComplexPlay(_audio))
                            {
                                if(!clip.IgnoreOneShotDelay)
                                    _nextOneShotAllowed = Time.time + _delayBetweenOneShots;
                                return;
                            }
                        }
                    }
                    break;
            }
        }
    }
}
