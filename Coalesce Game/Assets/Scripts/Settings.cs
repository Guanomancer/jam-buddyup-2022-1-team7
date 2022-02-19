using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class Settings : MonoBehaviour
    {
        [SerializeField]
        private GameSettings _gameSettings;
        public static GameSettings Game => _instance._gameSettings;

        #region Singleton Contract
        private static Settings _instance;

        private void Awake()
        {
            if(_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }
}
