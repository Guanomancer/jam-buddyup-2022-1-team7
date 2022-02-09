using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Coalesce
{
    public class BakeNavMeshOnPlay : MonoBehaviour
    {
        [SerializeField]
        private NavMeshSurface[] _surfaces;

        private void Awake()
        {
            foreach (var surface in _surfaces)
                surface.BuildNavMesh();
        }
    }
}
