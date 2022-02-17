using UnityEngine;

namespace Coalesce
{
    public class UIManager : MonoBehaviour
    {
        public GameObject _clock, _meter;

        private void Start()
        {
            _clock.SetActive(false);
            _meter.SetActive(false);
        }

        public void ActivateUI()
        {
            _clock.SetActive(true);
            _meter.SetActive(true);
        }
    }
}
