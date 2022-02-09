using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField]
        private GameSettings _gameSettings;

        public GameSettings GameSettings
            => _gameSettings;
    }
}
