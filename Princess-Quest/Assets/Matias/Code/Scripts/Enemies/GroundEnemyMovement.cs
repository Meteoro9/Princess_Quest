using UnityEngine;

public class GroundEnemyMovement : EnemiesMovementTest
{
    [Header("Ground Enemy Settings")]
    [SerializeField] private float _groundlinearDamping;

    protected override void ConfigurePhysics()
    {
        _rb.useGravity = true;
        _rb.linearDamping = _groundlinearDamping;
    }

    protected override void ApplyMovementForce(Vector3 direction)
    {
        Vector3 force = new Vector3(direction.x * _moveSpeed, 0, 0);
        _rb.AddForce(force);
    }

    protected override void LimitMaxSpeed()
    {
        Vector3 velocity = _rb.linearVelocity;
        if (Mathf.Abs(velocity.x) > _maxSpeed)
        {
            _rb.linearVelocity = new Vector3(Mathf.Sign(velocity.x) * _maxSpeed, velocity.y, 0);
        }
    }
}
