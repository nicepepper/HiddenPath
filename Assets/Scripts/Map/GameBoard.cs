using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CustomGameEvent;
using Map.Pathfinding;
using Random = UnityEngine.Random;

namespace Map
{
    public class GameBoard : MonoBehaviour
    {
        // [SerializeField] private Transform _startPoint;
        // [SerializeField] private Transform _endPoint;
        // [SerializeField] private Transform _startingPointOfThePlayingField;
        [SerializeField] private int _numberOfObstacles = 25;
        [SerializeField] private ObstacleSpawner _obstacleSpawner;
        private Point _fromPoint = new Point(0, 0);
        private Point _toPoint = new Point(9, 9);
        private int _gameBoardSizeX = 10;
        private int _gameBoardSizeY = 10;
        private int[,] _gameBoar;
        private List<Point> _emptyPositionPool = new List<Point>();
        private float _tileWidth = 2.5f;
        private float HalfWidthTheTile => _tileWidth / 2 ;
        
        private enum TypeTile
        {
            Road = 0,
            WayPoint = 1,
            DeadLock = 2,
            Obstacle = 10
        }

        private void Awake()
        {
            Initialization();
        }
        
        public Vector3 GetRandomPositionWithPathToEnd()
        {

            List<Point> path = null;
            Point point = new Point(0, 0);
            
            while (path == null && _emptyPositionPool.Count != 0)
            {
                var randomIndex = Random.Range(0, _emptyPositionPool.Count);
                path = Pathfinding.Pathfinding.FindPath(_gameBoar, _emptyPositionPool[randomIndex], _toPoint);
            
                if (path == null)
                {
                    _gameBoar[_emptyPositionPool[randomIndex].X, _emptyPositionPool[randomIndex].Y] = (int) TypeTile.DeadLock;
                }
                else
                {
                    _gameBoar[_emptyPositionPool[randomIndex].X, _emptyPositionPool[randomIndex].Y] = (int) TypeTile.WayPoint;
                }

                point = _emptyPositionPool[randomIndex];
                _emptyPositionPool.RemoveAt(randomIndex);
            }
            
            if (path == null)
            {
                return  new Vector3(-1f, -1f, -1f);
            }
            
            return new Vector3(HalfWidthTheTile + _tileWidth * point.X, HalfWidthTheTile, HalfWidthTheTile + _tileWidth * point.Y);
        }
        
        private void GetObstacle(int x, int y)
        {
            _obstacleSpawner.CreateObstacle(new Vector3(HalfWidthTheTile + _tileWidth * x, HalfWidthTheTile, HalfWidthTheTile + _tileWidth * y));
        }
        
        private void GetObstaclesWithRandomPositions()
        {
            for (int i = 0; i < _numberOfObstacles;)
            {
                var randomIndex = Random.Range(0, _emptyPositionPool.Count);
                _gameBoar[_emptyPositionPool[randomIndex].X, _emptyPositionPool[randomIndex].Y] = (int) TypeTile.Obstacle;
                var path = Pathfinding.Pathfinding.FindPath(_gameBoar, _fromPoint, _toPoint);
                
                if (path == null)
                {
                    _gameBoar[_emptyPositionPool[randomIndex].X, _emptyPositionPool[randomIndex].Y] = (int) TypeTile.Road;
                }
                else
                {
                    GetObstacle(_emptyPositionPool[randomIndex].X, _emptyPositionPool[randomIndex].Y);
                    _emptyPositionPool.RemoveAt(randomIndex);
                    ++i;
                }
            }
        }

        public void SpawnObstacles()
        {
            GetObstaclesWithRandomPositions();
        }

        public void ClearBoard()
        {
            _obstacleSpawner.Clear();
            Initialization();
            InitializePositionPool();
        }

        private void InitializePositionPool()
        {
            _emptyPositionPool.Clear();
            for (int i = 0; i < _gameBoardSizeX; i++)
            {
                for (int j = 0; j < _gameBoardSizeY; j++)
                {
                    _emptyPositionPool.Add(new Point(i, j));
                }
            }

            _emptyPositionPool.Remove(_fromPoint);
            _emptyPositionPool.Remove(_toPoint);
        }

       

        private void Initialization()
        {
            if (_gameBoar == null)
            {
                _gameBoar = new int[_gameBoardSizeX, _gameBoardSizeY];
            }
            
            for (int i = 0; i < _gameBoar.GetLength(0); i++)
            {
                for (int j = 0; j < _gameBoar.GetLength(1); j++)
                {
                    _gameBoar[i, j] = (int)TypeTile.Road;
                }
            }
        }
    }
}
