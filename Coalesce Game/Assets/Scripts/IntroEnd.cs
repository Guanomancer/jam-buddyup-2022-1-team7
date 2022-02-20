using UnityEngine;
using UnityEngine.SceneManagement;

namespace Coalesce
{
    public class IntroEnd : MonoBehaviour
    {
        [SerializeField]
        private float _videoLength;

        void Update()
        {
            if(Time.time > _videoLength)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
