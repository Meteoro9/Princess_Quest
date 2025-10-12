using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] float _detectionRange;
    [SerializeField] float _attackRange;
    //[SerializeField] LayerMask _playerLayer;
    [SerializeField] LayerMask _obstacleLayer;
    
    [Header("Patrol")]
    [SerializeField] Vector3[] _patrolPoints;
    [SerializeField] float _pointReachedDistance;
    
    private EnemiesMovementTest _movement;
    private Transform _player;
    private bool _isChasing = false;
    private int _currentPatrolIndex = 0;

    void Start()
    {
        _movement = GetComponent<EnemiesMovementTest>();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;
            
        if (_patrolPoints != null && _patrolPoints.Length > 0)
        {
            _movement.SetTargetWithDelay(_patrolPoints[_currentPatrolIndex]);
        }
    }

    void Update()
    {
        if (_player == null) 
        {
            //Si no hay jugador solo patrulla
            PatrolBehavior();
            return;
        }

        bool canSeePlayer = CheckPlayerDetection();
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        
        if (canSeePlayer)
        {
            _isChasing = true;
            
            if (distanceToPlayer > _attackRange)
            {
                _movement.ResumeMovement();
                _movement.SetTarget(_player.position);
            }
            else
            {
                _movement.StopMovement();
            }
        }
        else if (_isChasing)
        {
            _isChasing = false;
            ResumePatrol();
        }
        else
        {
            PatrolBehavior();
        }
    }

    bool CheckPlayerDetection()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (distanceToPlayer > _detectionRange) return false;

        Vector3 directionToPlayer = (_player.position - transform.position).normalized;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, _detectionRange, _obstacleLayer))
        {
            return hit.collider.CompareTag("Player");
        }

        return true;
    }

    void PatrolBehavior()
    {
        if (_patrolPoints == null || _patrolPoints.Length == 0) return;

        Vector3 currentTarget = _patrolPoints[_currentPatrolIndex];
        
        if (Vector3.Distance(transform.position, currentTarget) <= _pointReachedDistance)
        {
            GoToNextPatrolPoint();
        }
    }

    private void GoToNextPatrolPoint()
    {
        _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
        _movement.SetTargetWithDelay(_patrolPoints[_currentPatrolIndex]);
    }

    private void ResumePatrol()
    {
        if (_patrolPoints != null && _patrolPoints.Length > 0)
        {
            _movement.ResumeMovement();
            
            //Encontrar el punto mas cercano
            float closestDistance = float.MaxValue;
            int closestIndex = 0;
            
            for (int i = 0; i < _patrolPoints.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, _patrolPoints[i]);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }
            
            _currentPatrolIndex = closestIndex;
            _movement.SetTargetWithDelay(_patrolPoints[_currentPatrolIndex]);
        }
    }

    //Metodos publicos para el ataque 
    public void ForceStopForAttack(float stopDuration)
    {
        _movement.StopMovement();
        StartCoroutine(ResumeAfterAttack(stopDuration));
    }

    private IEnumerator ResumeAfterAttack(float stopDuration)
    {
        yield return new WaitForSeconds(stopDuration);
        
        if (!_isChasing)
        {
            _movement.ResumeMovement();
            
            if (!_isChasing && _patrolPoints != null && _patrolPoints.Length > 0)
            {
                _movement.SetTargetWithDelay(_patrolPoints[_currentPatrolIndex]);
            }
        }
    }

    //Gizmos
    void OnDrawGizmosSelected()
    {
        //Rangos basicos
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    
        //Puntos de patrullaje
        if (_patrolPoints != null && _patrolPoints.Length > 0)
        {
            Gizmos.color = Color.green;
        
            for (int i = 0; i < _patrolPoints.Length; i++)
            {
                Gizmos.DrawWireCube(_patrolPoints[i], Vector3.one * 0.3f);
            }
        }
    }

    public bool IsChasing() => _isChasing; //Saber si esta persigiendo al Player
    public void SetPatrolPoints(Vector3[] points) => _patrolPoints = points; //Agregar un punto de patrullaje
}