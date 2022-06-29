using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabObstacle;
        [SerializeField] private Transform _startPoint;
        
        private List<GameObject> _obstacles = new List<GameObject>();
        
        
    }
}
