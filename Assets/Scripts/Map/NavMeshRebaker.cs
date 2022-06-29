using UnityEngine;
using UnityEngine.AI;

namespace Map
{
    [RequireComponent(typeof(NavMeshSurface))]
    public class NavMeshRebaker : MonoBehaviour
    {
        private NavMeshSurface _navMeshSurface;

        private void Awake()
        {
            _navMeshSurface = GetComponent<NavMeshSurface>();
        }

        public void Rebake()
        {
            _navMeshSurface.BuildNavMesh();
        }
    }
}
