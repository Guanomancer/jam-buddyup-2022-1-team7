using UnityEngine;
using UnityEngine.SceneManagement;

namespace Coalesce
{
    public class TimedSceneAdvance : MonoBehaviour
    {
        [SerializeField]
        private float _seconds;

        private float _startTime;

        private void OnEnable()
        {
            _startTime = Time.time;
        }

        void Update()
        {
            if (Time.time - _startTime > _seconds)
                Advance();
        }

        public void Advance()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
