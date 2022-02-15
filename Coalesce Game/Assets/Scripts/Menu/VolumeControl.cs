using UnityEngine;
using UnityEngine.UI;

namespace Coalesce
{
    public class VolumeControl : MonoBehaviour
    {
        void Update()
        {
            PlayerPrefs.SetFloat("volume", GetComponent<Scrollbar>().value);
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }
    }
}
