using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class Clock : MonoBehaviour
    {
        public float _seconds;

        private GameObject _hand;

        private void Start()
        {
            _hand = GameObject.Find("Hand");
        }

        private void Update()
        {
            _hand.transform.Rotate(new Vector3(0, 0, (Time.deltaTime * -360) / _seconds));
        }
    }
}
