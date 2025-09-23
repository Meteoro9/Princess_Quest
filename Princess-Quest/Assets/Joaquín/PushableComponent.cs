using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableComponent : MonoBehaviour, IHurtbox
{
    [Range(0.1f, 10)]
    [SerializeField]
    float weight = 1;

    public void OnHurtboxHit(HitboxData hitboxData)
    {
        Push(hitboxData.Direction, hitboxData.PushForce, hitboxData.Hitter);
    }

    public void Push(Vector3 direction, int pushForce, GameObject hitter)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 dir = direction.normalized;
        Debug.Log("direction of push : " + dir);

        Debug.Log(
            "is hitter to the right of "
                + gameObject.name
                + " : "
                + (hitter.transform.position.x > transform.position.x)
        );
        if (hitter.transform.position.x > transform.position.x)
        {
            dir.x *= -1;
        }
        Debug.Log("Force added to the rigidbody: " + dir * pushForce / weight);

        rb.AddForce(dir * pushForce / weight);
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
