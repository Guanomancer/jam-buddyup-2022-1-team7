using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.DeadCode
{
    public class PersistanceManager : ManagerBase<PersistanceManager>
    {
        private const string FIRST_RUN_NAME = "FirstRun";
        private const string VOLUME_NAME = "Volume";

        private bool _isInitialized;

        private void Start()
            => Initialize();

        private void Initialize()
        {
            if (_isInitialized)
                return;
            
            Debug.Log("First run: " + PlayerPrefs.GetInt(FIRST_RUN_NAME));
            if (PlayerPrefs.GetInt(FIRST_RUN_NAME) == 0)
            {
                PlayerPrefs.SetInt(FIRST_RUN_NAME, 1);
                PlayerPrefs.SetFloat(VOLUME_NAME, 1f);
            }
            AudioListener.volume = PlayerPrefs.GetFloat(VOLUME_NAME);

            _isInitialized = true;
        }

        public float Volume
        {
            get
            {
                if (!_isInitialized)
                    Initialize();
                return PlayerPrefs.GetFloat(VOLUME_NAME);
            }
            set
            {
                var volume = Mathf.Clamp(value, 0, 1);
                PlayerPrefs.SetFloat(VOLUME_NAME, volume);
                AudioListener.volume = volume;
            }
        }
    }
}
