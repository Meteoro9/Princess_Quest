using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public abstract class EnemiesMovementTest : MonoBehaviour
{
    [Header("Movement Settings")]    
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _maxSpeed;
    [SerializeField] protected float _targetMovementDelay;
    [SerializeField] protected Vector3 _target;
    
    protected Rigidbody _rb;
    protected Vector3 _currentDirection;
    protected MovementState _currentState = MovementState.Moving;
    protected Vector3 _baseScale;

    public enum MovementState
    {
        Moving,
        Stopped,
        Paused
    }

    protected virtual void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _baseScale = transform.localScale;
        
        ConfigurePhysics();
    }


    public virtual void MoveTo(Vector3 targetPos)
    {
        if (_currentState == MovementState.Stopped) return;

        Vector3 dir = CalculateDirection(targetPos);
        
        if (dir.magnitude > 0.1f)
        {
            dir.Normalize();
            _currentDirection = dir;
            
            ApplyMovementForce(dir);
            LimitMaxSpeed();
            UpdateFacingDirection();
        }
    }

    protected virtual Vector3 CalculateDirection(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.z = 0;
        return dir;
    }

    protected virtual void LimitMaxSpeed()
    {
        Vector3 velocity = _rb.linearVelocity;
        Vector3 limitedVelocity = velocity;
        
        //Limita la velocidad en X
        if (Mathf.Abs(velocity.x) > _maxSpeed)
        {
            limitedVelocity.x = Mathf.Sign(velocity.x) * _maxSpeed;
        }
        
        _rb.linearVelocity = limitedVelocity;
    }

    protected virtual IEnumerator PauseMovementRoutine(float pauseTime)
    {
        _currentState = MovementState.Paused;
        
        yield return new WaitForSeconds(pauseTime);
        
        if (_currentState == MovementState.Paused)
        {
            _currentState = MovementState.Moving;
        }
    }

    protected virtual IEnumerator SetTargetAfterDelay(Vector3 newTarget, float delay)
    {
        _currentState = MovementState.Paused;

        yield return new WaitForSeconds(delay);
        
        _target = newTarget;
        _currentState = MovementState.Moving;
    }

    protected virtual void UpdateFacingDirection()
    {
        if (_currentDirection.x > 0)
            transform.localScale = _baseScale;
        else if (_currentDirection.x < 0)
            transform.localScale = new Vector3(-_baseScale.x, _baseScale.y, _baseScale.z);
    }

    protected virtual void FixedUpdate()
    {
        if (_target != Vector3.zero && _currentState == MovementState.Moving)
        {
            MoveTo(_target);
        }

        if (_currentState == MovementState.Moving)
        {
            LimitMaxSpeed();
        }
    }

    //Metodos Abstractos
    protected abstract void ConfigurePhysics(); //Especifica de cada tipo de enemigo
    protected abstract void ApplyMovementForce(Vector3 direction);

    //Metodos Publicos
    public virtual void SetTargetWithDelay(Vector3 newTarget)
    {
        StartCoroutine(SetTargetAfterDelay(newTarget, _targetMovementDelay));
    }

     public virtual void StopMovement()
    {
        _currentState = MovementState.Stopped;
        _rb.linearDamping = 10;
        _target = transform.position;
    }

    public virtual void ResumeMovement()
    {
        _currentState = MovementState.Moving;
        ConfigurePhysics();
    }

    public virtual void PauseMovement(float pauseTime)
    {
        StartCoroutine(PauseMovementRoutine(pauseTime));
    }

    public virtual void SetTarget(Vector3 newTarget) => _target = newTarget; //Cambia de objetivo de forma inmediata
    public Vector3 GetCurrentTarget() => _target; //Muestra la posicion a la que se dirige 
    public MovementState GetCurrentState() => _currentState; //Ve el estado en el que se encuentra
}