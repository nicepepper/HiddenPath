using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CustomGameEvent
{
    public static class GameEvent
    {
        public static readonly UnityEvent<Vector3> OnMaxNoize = new UnityEvent<Vector3>();
        public static readonly UnityEvent<bool> OnMovePlayer = new UnityEvent<bool>();
        
        private static Vector3 _playerPosition = new Vector3();
        private static GameStage _current = GameStage.LOADED;
        private static readonly  Dictionary<GameStage, UnityEvent> _actions = new Dictionary<GameStage, UnityEvent>();
        private static readonly UnityEvent _onChangedStage = new UnityEvent();

        public static void SendMaxNoize()
        {
            OnMaxNoize.Invoke(_playerPosition);
        }

        public static Vector3 GetPlayerPosition()
        {
            return _playerPosition;
        }

        public static void SendMovePlayer(Vector3 position)
        {
            if (_playerPosition.Equals(position))
            {
                OnMovePlayer.Invoke(false);
            }
            else
            {
                OnMovePlayer.Invoke(true);
            }
            _playerPosition = position;
        }
        
        
        public static GameStage Current
        {
            get => _current;
            set
            {
                _current = value;
                Invoke(value);
            }
        }

        public static event UnityAction OnChangedStage
        {
            add => _onChangedStage.AddListener(value);
            remove => _onChangedStage.RemoveListener(value);
        }

        public static event UnityAction OnPrepare
        {
            add => AddListener(GameStage.PREPARE, value);
            remove => RemoveListener(GameStage.PREPARE, value);
        }
        
        public static event UnityAction OnStart
        {
            add => AddListener(GameStage.START, value);
            remove => RemoveListener(GameStage.START, value);
        }
        
        public static event UnityAction OnStop
        {
            add => AddListener(GameStage.STOP, value);
            remove => AddListener(GameStage.STOP, value);
        }
        
        public static event UnityAction OnQuit
        {
            add => AddListener(GameStage.QUIT, value);
            remove => RemoveListener(GameStage.QUIT, value);
        }
        
        public static event UnityAction OnWin
        {
            add => AddListener(GameStage.WIN, value);
            remove => RemoveListener(GameStage.WIN, value);
        }
        
        public static event UnityAction OnLose
        {
            add => AddListener(GameStage.LOSE, value);
            remove => RemoveListener(GameStage.LOSE, value);
        }
        
       private static void AddListener(GameStage stage, UnityAction action)
        {
            if (_actions.TryGetValue(stage, out var unityEvent))
            {
                unityEvent.AddListener(action);
            }
            else
            {
                unityEvent = new UnityEvent();
                unityEvent.AddListener(action);
                _actions[stage] = unityEvent;
            }
            if (Current == stage)
            {
                action.Invoke();
            }
        }

        private static void RemoveListener(GameStage stage, UnityAction action)
        {
            if (_actions.TryGetValue(stage, out var unityEvent))
            {
                unityEvent.RemoveListener(action);
            }
        }

        private static void Invoke(GameStage stage)
        {
            _onChangedStage.Invoke();
            if (_actions.TryGetValue(stage, out var unityAction))
            {
                unityAction.Invoke();
            }
        }
    }
}
