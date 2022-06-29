using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Map
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabObstacle;
        [SerializeField] private Transform _startPoint;
        
        private int [,] _grid;
        private int _gridSizeX = 10;
        private int _gridSizeY = 10;
        private int _numberOfObstacles = 21;
        private List<GameObject> _obstacles = new List<GameObject>();
        
        private enum TypeTile
        {
            Road = 0,
            Obstacle = 5
        }

        private void Awake()
        {
            Initialization();
        }

        private void Start()
        {
            //делал это из последних сил, хотел спатьно, пусть хоть что то работает, буду доделывать, тут все поменяется ;) + Enemy patrol
            
            // var path = Pathfinding.FindPath(_grid, new Point(0, 0), new Point(9, 9));
            // foreach (var point in path)
            // {
            //     Debug.Log("point [" + point.X + ", " + point.Y + "]!");
            // }
            
            GetPointObstacles();
            
        }

        private void GenerateObstacles(int x, int y)
        {
             _obstacles.Add(Instantiate(_prefabObstacle, new Vector3(1.25f + 2.5f * x, 1.25f, 1.25f + 2.5f * y), Quaternion.identity));
        }

        private void onDestroy()
        {
            foreach (var obstacle in _obstacles)
            {
                Destroy(obstacle);
            }
        }

        private void GetPointObstacles()
        {
            int x;
            int y;
            List<Point> path;
            
            for (int i = 0; i < _numberOfObstacles;)
            {
                x = Random.Range(0, 10);
                y = Random.Range(0, 10);
                _grid[x, y] = (int) TypeTile.Obstacle;
                Debug.Log(i + " x->" + x + ", y->" + y);
                path =  Pathfinding.FindPath(_grid, new Point(0, 0), new Point(9, 9));
                if (path == null)
                {
                    
                    _grid[x, y] = (int) TypeTile.Obstacle;
                }
                else
                {
                    GenerateObstacles(x, y);
                    i++;
                }
            }
        }

        private void MakeRandomPoints(ref int[,] grid)
        {
            
        }

        private void Initialization()
        {
            _grid = new int[_gridSizeX, _gridSizeY];
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    _grid[i, j] = (int)TypeTile.Road;
                }
            }
        }
        
        
    }
}
