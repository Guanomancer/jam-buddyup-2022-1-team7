using UnityEngine;
using UnityEngine.UI;

namespace Coalesce
{
    public class SoundToggle : MonoBehaviour
    {
        public bool muteButton;
        public GameObject _otherButton;
        public Sprite _highlightedImage, _nonHighlightedImage;
        public Scrollbar _volumeControl;
        
        public float _cachedVolume = 1;

        private void Update()
        {
            if (muteButton && _volumeControl.value > 0)
                VolumeToggle(false);
            else if (!muteButton && _volumeControl.value == 0)
                VolumeToggle(false);
        }

        public void VolumeToggle(bool check)
        {


            _otherButton.SetActive(true);
            gameObject.GetComponent<Image>().sprite = _highlightedImage;
            _otherButton.GetComponent<Image>().sprite = _nonHighlightedImage;
            gameObject.SetActive(false);

            if (!check)
                return;


            if (_volumeControl.value > 0)
            {
                _cachedVolume = _volumeControl.value;
                _otherButton.GetComponent<SoundToggle>()._cachedVolume = _cachedVolume;
                _volumeControl.value = 0;
            }
            else
                _volumeControl.value = _cachedVolume;
        }
    }
}
