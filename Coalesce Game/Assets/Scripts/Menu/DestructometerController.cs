using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Coalesce
{
    public class DestructometerController : MonoBehaviour
    {
        [SerializeField]
        private Image _fillImage;

        [SerializeField]
        private Image[] _powerupImages;
        [SerializeField]
        private float[] _powerupPercentages;

        [SerializeField, Range(0, 1)]
        private float _destruction = 1f;
        public float Destruction
        {
            get => _destruction;
            set
            {
                _destruction = Mathf.Clamp(value, 0, 1);
                UpdateFill();
            }
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
    }
}
