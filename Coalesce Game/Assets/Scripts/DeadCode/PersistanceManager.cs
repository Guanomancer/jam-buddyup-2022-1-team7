using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class PersistanceManager : ManagerBase<PersistanceManager>
    {
        private const string FIRST_RUN_NAME = "FirstRun";
        private const string VOLUME_NAME = "Volume";

        private void Start()
        {
            if(PlayerPrefs.GetInt(FIRST_RUN_NAME) == 0)
            {
                PlayerPrefs.SetInt(FIRST_RUN_NAME, 1);
                PlayerPrefs.SetFloat(VOLUME_NAME, 1f);
            }
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
