using UnityEngine;

public class FlyingEnemyMovement : EnemiesMovementTest
{
    [Header("Flying Enemy Settings")]
    [SerializeField] private float _flyinglinearDamping;

    protected override void ConfigurePhysics()
    {
        _rb.useGravity = false;
        _rb.linearDamping = _flyinglinearDamping;
    }

    protected override void ApplyMovementForce(Vector3 direction)
    {
        Vector3 force = new Vector3(direction.x * _moveSpeed, direction.y * _moveSpeed, 0);
        _rb.AddForce(force);
    }

    protected override void LimitMaxSpeed()
    {
        Vector3 velocity = _rb.linearVelocity;
        Vector3 limitedVelocity = velocity;
        
        if (Mathf.Abs(velocity.x) > _maxSpeed)
        {
            limitedVelocity.x = Mathf.Sign(velocity.x) * _maxSpeed;
        }
        
        if (Mathf.Abs(velocity.y) > _maxSpeed)
        {
            limitedVelocity.y = Mathf.Sign(velocity.y) * _maxSpeed;
        }
        
        _rb.linearVelocity = limitedVelocity;
    }
}