using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Coalesce.DeadCode
{
    public class ObjectEnablerZone : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _size = Vector3.one;

        [SerializeField]
        private GameObject _object;

        [SerializeField]
        private UnityEvent _onTrigger;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, _size);
        }

        private void Update()
        {
            var colliders = Physics.OverlapBox(transform.position, _size / 2f, transform.rotation);
            foreach(var collider in colliders)
            {
                if(collider.GetComponentInParent<TodzillaController>() == null)
                    continue;

                _object.SetActive(true);
                enabled = false;
                _onTrigger.Invoke();
                GameManager.Instance.StartGame();
            }
        }
    }
}
