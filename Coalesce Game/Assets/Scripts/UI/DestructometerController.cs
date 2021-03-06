using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Coalesce.EventRouting;

namespace Coalesce.UI
{
    public class DestructometerController : MonoBehaviour, IEventSubscriber
    {
        [SerializeField]
        private Image _fillImage;

        [SerializeField]
        private Image[] _powerupImages;
        [SerializeField]
        private float[] _powerupPercentages;

        [SerializeField, Range(0, 1)]
        private static float _destruction = 0f;
        public static float Destruction
        {
            get => _destruction;
            set => _destruction = Mathf.Clamp(value, 0, 1);
        }

        [SerializeField]
        private UnityEvent _onPowerUp;

        private float _fullSize;

        public void ResetDestruction()
        {
            _destruction = 1f;
            foreach (var image in _powerupImages)
                image.enabled = true;
            UpdateFill();
        }

        private void Awake()
            => _fullSize = _fillImage.rectTransform.sizeDelta.y;

        private void Update()
            => UpdateFill();

        private void UpdateFill()
        {
            _fillImage.rectTransform.sizeDelta = new Vector2(_fillImage.rectTransform.sizeDelta.x, _destruction * _fullSize);
            for (int i = 0; i < _powerupImages.Length && i < _powerupPercentages.Length; i++)
            {
                if (_powerupImages[i].enabled)
                {
                    if (_destruction <= _powerupPercentages[i])
                    {
                        _powerupImages[i].enabled = false;
                        _onPowerUp.Invoke();
                    }
                }
            }
        }

        private void OnEnable()
            => EventRouter.Subscribe<EventTypes.ScoringBlockMessy>(this);
        private void OnDisable()
            => EventRouter.Unsubscribe<EventTypes.ScoringBlockMessy>(this);

        public void OnEvent<T>(T eventData) where T : IEventData
        {
            switch(eventData)
            {
                case EventTypes.ScoringBlockMessy data:
                    Destruction = data.DestructionRatio;
                    break;
            }
        }
    }
}
