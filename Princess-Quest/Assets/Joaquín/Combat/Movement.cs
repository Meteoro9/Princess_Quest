using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    Vector3 lastDirection;
    Rigidbody rb;

    [SerializeField]
    float _speed = 2.0f;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveInDirection(Vector3 direction)
    {
        float velY = rb.linearVelocity.y;
        rb.linearVelocity = direction * _speed;
        rb.linearVelocity += new Vector3(0, velY);

        SetLastDirection(direction);
    }

    public void MoveRight()
    {
        MoveInDirection(Vector3.right);
    }

    public void MoveLeft()
    {
        MoveInDirection(Vector3.left);
    }

    void SetLastDirection(Vector3 direction)
    {
        if (direction.x > 0)
        {
            lastDirection = Vector3.right;
        }
        else if (direction.x < 0)
        {
            lastDirection = Vector3.left;
        }
    }
}
