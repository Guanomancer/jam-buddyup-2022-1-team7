using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Coalesce
{
    public class MouseOverSound : MonoBehaviour, IPointerEnterHandler
    {
        private AudioSource _audio;

        private void Start()
            => _audio = GetComponent<AudioSource>();

        public void OnPointerEnter(PointerEventData eventData)
            => _audio.Play();
    }
}
