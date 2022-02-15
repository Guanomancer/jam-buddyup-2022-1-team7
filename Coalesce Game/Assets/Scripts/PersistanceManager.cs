using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class PersistanceManager : ManagerBase<PersistanceManager>
    {
        private const string VOLUME_NAME = "Volume";

        private void Start()
        {
            AudioListener.volume = PlayerPrefs.GetFloat(VOLUME_NAME);
        }

        public float Volume
        {
            get => PlayerPrefs.GetFloat(VOLUME_NAME);
            set
            {
                var volume = Mathf.Clamp(value, 0, 1);
                PlayerPrefs.SetFloat(VOLUME_NAME, volume);
                AudioListener.volume = volume;
            }
        }
    }
}
