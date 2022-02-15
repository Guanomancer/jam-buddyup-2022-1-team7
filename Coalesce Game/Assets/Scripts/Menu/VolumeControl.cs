using UnityEngine;
using UnityEngine.UI;

namespace Coalesce
{
    public class VolumeControl : MonoBehaviour
    {
        private void Start()
            => GetComponent<Scrollbar>().value = PersistanceManager.Instance.Volume;

        void Update()
            => PersistanceManager.Instance.Volume = GetComponent<Scrollbar>().value;
    }
}
