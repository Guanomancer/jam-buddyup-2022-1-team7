using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Coalesce.EventRouting;

namespace Coalesce.UI
{
    public class Clock : MonoBehaviour
    {
        [SerializeField]
        private Image _fillImage;

        private float _startTime;
        private bool _isRunning;

        private GameObject _hand;

        public void StartClock()
        {
            _startTime = Time.time;
            _isRunning = true;
        }

        public void StopClock()
            => _isRunning = false;

        private void Start()
        {
            _hand = GameObject.Find("Hand");
        }

        private void Update()
        {
            if (!_isRunning)
                return;

            _hand.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * -360) / Settings.Game.GameLengthInSeconds));
            _fillImage.fillAmount = 1f - ((Time.time - _startTime) / Settings.Game.GameLengthInSeconds);

            if (_fillImage.fillAmount <= 0)
            {
                StopClock();
                EventRouter.Dispatch(new EventTypes.TimesIsUp { });
                EventRouter.Dispatch(new EventTypes.GameEnd { });
            }
        }
    }
}
