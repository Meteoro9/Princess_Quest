using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableComponent : MonoBehaviour
{
    [Range(0.1f, 10)]
    [SerializeField]
    float weight = 1;

    public void Push(Transform trans)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 dir = (transform.position - trans.position).normalized;

        rb.AddForce(dir * 500 / weight);
    }
}
