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
    // TODO make this work
    /*     public void Push(HitboxData hitboxData)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 dir = hitboxData.Direction.normalized;
            int pForce = hitboxData.PushForce;
    
            rb.AddForce(dir * pForce / weight);
        } */
}
