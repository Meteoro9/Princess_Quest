using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float acceleration;

    [SerializeField]
    GameObject target;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = maxSpeed;
    }

    public void MoveTo(Vector3 worldPos)
    {
        Vector3 dir = worldPos - transform.position;
        rb.AddForce(dir * acceleration);
    }

    // TODO make this component not dependent on target, create component for player detection.
    void FixedUpdate()
    {
        MoveTo(target.transform.position);
    }
}
