using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Coalesce.DeadCode
{
    public class Clock : MonoBehaviour
    {
        [SerializeField]
        private float _fullTime = 180f;
        [SerializeField]
        private Image _fillImage;

        private float _startTime;
        private bool _isRunning;

        public float _seconds;

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

            _hand.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * -360) / _seconds));
            _fillImage.fillAmount = 1f - ((Time.time - _startTime) / _fullTime);

            if(_fillImage.fillAmount <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
