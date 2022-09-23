using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabObstacle; 
        private List<GameObject> _obstacles = new List<GameObject>();

        public void CreateObstacle(Vector3 position)
        {
            _obstacles.Add(Instantiate(_prefabObstacle, position, Quaternion.identity));
        }

        public void Clear()
        {
            foreach (var obstacle in _obstacles)
            {
                if (obstacle != null)
                {
                    Destroy(obstacle);
                }
            }
        }
    }
}
