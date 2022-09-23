using System.Collections.Generic;
using CustomGameEvent;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _guardTime = 3f;
        private float _elapsedTime = 0f;
        private NavMeshAgent _agent;
        private FieldOfView _fieldOfView;
        private Vector3 CurrentTarget { get; set; }
        private List<Vector3> _patrolPoints = new List<Vector3>();
        private int _nextPatrolPoint;
        private int _previousPatrolPoint;
        private Stage _currentStage;
        
        private enum Stage
        {
            PATROL,
            GUARD,
            CHASE
        }
        private float MAX_STOP_DISTANCE_TO_TARGET = 0.5f;
        
        public bool GameUpdate()
        {
            DetectionOnFieldOfView();
            switch (_currentStage)
            {
                case Stage.PATROL:
                    ProcessPatrol();
                    break;
                case Stage.GUARD:
                    ProcessGuard();
                    break;
                case Stage.CHASE:
                    ProcessChase();
                    break;
            }
            
            return true;
        }
        
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void AddPatrolPoint(Vector3 position)
        {
            _patrolPoints.Add(position);
        }

        public void Stop()
        {
            _agent.isStopped = _agent.isStopped != true;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _fieldOfView = GetComponent<FieldOfView>();
            _currentStage = Stage.GUARD;
            _nextPatrolPoint = 0;
            _previousPatrolPoint = 0;
            if (_patrolPoints.Count != 0)
            {
                _agent.Warp(_patrolPoints[0]);
            }
            
            GameEvent.OnMaxNoize.AddListener(posPlayer =>
            {
                CurrentTarget = posPlayer;
                _currentStage = Stage.CHASE;
            });
        }
        
        private void ProcessPatrol()
        {
            if (_patrolPoints.Count > 1)
            {
                UpdateDestination();
                if (Vector3.Distance(transform.position, _patrolPoints[_nextPatrolPoint]) < MAX_STOP_DISTANCE_TO_TARGET)
                {
                    _agent.ResetPath();
                    _currentStage = Stage.GUARD;
                }
               
                return;
            }

            if (_patrolPoints.Count == 0 || _patrolPoints.Count == 1)
            {
                _currentStage = Stage.GUARD;
            }
        }
         private void ProcessGuard()
         {
             _elapsedTime += Time.deltaTime;
             if (_elapsedTime >= _guardTime)
             {
                 _elapsedTime = 0;
                 _currentStage = Stage.PATROL;
                 if (_patrolPoints.Count > 1)
                 {
                     IterateNextPointIndex();
                 }
             }
         }
         
         private void ProcessChase()
         {
             CurrentTarget = GameEvent.GetPlayerPosition();
             _agent.SetDestination(CurrentTarget);
         }

         private void UpdateDestination()
         {
             if (_previousPatrolPoint != _nextPatrolPoint)
             {
                 _previousPatrolPoint = _nextPatrolPoint;
                 _agent.SetDestination(_patrolPoints[_nextPatrolPoint]);
             }
         }

         private void IterateNextPointIndex()
         {
             _nextPatrolPoint++;
             if (_nextPatrolPoint == _patrolPoints.Count)
             {
                 _nextPatrolPoint = 0;
             }
         }

         private void DetectionOnFieldOfView()
         {
             if (_fieldOfView.VisibleTargets.Count != 0)
             {
                 _currentStage = Stage.CHASE;
             }
         }
    }
}



