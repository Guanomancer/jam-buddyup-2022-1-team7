using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _scoreScreen;

        public void Continue()
        {
            _scoreScreen.SetActive(true);
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}
