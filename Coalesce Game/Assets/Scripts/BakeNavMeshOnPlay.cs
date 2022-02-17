using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Coalesce
{
    public class BakeNavMeshOnPlay : MonoBehaviour
    {
        public MeshRenderer m;

        [SerializeField]
        private NavMeshSurface[] _surfaces;

        private void Awake()
        {
            m.enabled = false;

            foreach (var surface in _surfaces)
            {
                if (surface.tag != "Door")
                    surface.BuildNavMesh();
            }

            m.enabled = true;
        }
    }
}
