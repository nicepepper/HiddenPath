using System.Collections.Generic;
using CustomGameEvent;
using UnityEngine;
using Enemy;
using Map;
using Unity.VisualScripting;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerController _prefabPlayer;
        [SerializeField] private Enemy.Enemy _prefabEnemy;
        [SerializeField] private int _numberOfWaypoints = 2;
        [SerializeField] private int _numberOfEnemies = 2;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private NavMeshRebaker _navMesh;
        [SerializeField] private GameBoard _board;
        private PlayerController CurrentPlayer { get; set; }
        private PlayerMovement _playerMovement;
        private EnemyCollection _enemies = new EnemyCollection();

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            GameEvent.OnPrepare += OnGamePrepare;
            GameEvent.OnStart += OnGameStart;
            GameEvent.OnQuit += OnGameQuit;
            GameEvent.OnChangedStage += OnChangedGameStage;
        }

        private void Update()
        {
            if (GameEvent.Current == GameStage.START)
            {
                _enemies.GameUpdate();
            }
        }

        private void OnDestroy()
        {
            GameEvent.OnPrepare -= OnGamePrepare;
            GameEvent.OnStart -= OnGameStart;
            GameEvent.OnQuit -= OnGameQuit;
            GameEvent.OnChangedStage -= OnChangedGameStage;
        }

        private void CheckGameOver()
        {
            
        }
        
        private void OnGamePrepare()
        {
            if (CurrentPlayer != null)
            {
                CurrentPlayer.Destroy();
            }
            
            _board.ClearBoard();
            _enemies.DestroyEnemies();
        }

        private void OnGameStart()
        {
            _board.SpawnObstacles();
            _navMesh.Rebake();
            if (CurrentPlayer != null)
            {
                CurrentPlayer.Destroy();
            }
            
            CurrentPlayer = Instantiate(_prefabPlayer, _startPoint.position, _startPoint.rotation);
            if (CurrentPlayer != null)
            {
                _playerMovement.SetPlayerController(CurrentPlayer);
            }
            
            EnemySpawn();
        }
        
        private void OnGameQuit()
        {
            Application.Quit();
        }

        private void OnChangedGameStage()
        {
            if (GameEvent.Current != GameStage.START)
            {
                _enemies.Stop();
            }
        }

        private void EnemySpawn()
        {
            var wayPoints = new List<Vector3>();
            GetWayPoint(wayPoints);
            Enemy.Enemy curEnemy = Instantiate(_prefabEnemy, wayPoints[0], Quaternion.identity);
            SetWaypoints(curEnemy, wayPoints);
            _enemies.Add(curEnemy);
        }

        private void GetWayPoint(List<Vector3> points)
        {
            if (_numberOfWaypoints != 0)
            {
                for (int i = 0; i < _numberOfWaypoints; i++)
                {
                    var position = _board.GetRandomPositionWithPathToEnd();
                    var badPosition = new Vector3(-1f, -1f, -1f);
                    if (position != badPosition)
                    {
                        points.Add(position);
                    }
                }
            }
        }
        
        private void SetWaypoints(Enemy.Enemy enemy, List<Vector3> wayPoints)
        {
            foreach (var point in wayPoints)
            {
                enemy.AddPatrolPoint(point);
            }
        }
    }
}
