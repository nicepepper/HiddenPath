using System;
using System.Collections.Generic;
using CustomGameEvent;
using Unity.VisualScripting;
using UnityEngine;
using Enemy;
using Map;
using UnityEngine.AI;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerController _prefabPlayer;
        [SerializeField] private Enemy.Enemy _prefabEnemy;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private NavMeshRebaker _navMesh;
    
        private PlayerController _currentPlayer { get; set; }
        private PlayerMovement _playerMovement;
        private EnemyCollection _enemies = new EnemyCollection();

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            GameEvent.OnPrepare += OnGamePrepare;
            GameEvent.OnStart += OnGameStart;
            GameEvent.OnQuit += OnGameQuit;
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
        }

        private void CheckGameOver()
        {
            
        }
        
        private void OnGamePrepare()
        {
            if (_currentPlayer != null)
            {
                _currentPlayer.Destroy();
            }
            _enemies.DestroyEnemies();
        }

        private void OnGameStart()
        {
            _navMesh.Rebake();

            if (_currentPlayer != null)
            {
                _currentPlayer.Destroy();
            }
            
            _currentPlayer = Instantiate(_prefabPlayer, _startPoint.position, _startPoint.rotation);
            if (_currentPlayer != null)
            {
                _playerMovement.SetPlayerController(_currentPlayer);
            }
            
            EnemySpawn(new Vector3(5f,5f,5f));
        }
        
        private void OnGameQuit()
        {
            Application.Quit();
        }

        private void EnemySpawn(Vector3 _position)
        {
            _enemies.Add(Instantiate(_prefabEnemy, _position, Quaternion.identity));
        }
    }
}
