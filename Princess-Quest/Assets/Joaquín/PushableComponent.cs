using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableComponent : MonoBehaviour, IHurtbox
{
    [Range(0.1f, 10)]
    [SerializeField]
    float weight = 1;

    [SerializeField]
    bool isHurtboxEventActive = true;
    public bool IHurtboxActive { get; set; }

    void Awake()
    {
        IHurtboxActive = isHurtboxEventActive;
    }

    public void OnHurtboxHit(HitboxData hitboxData)
    {
        Push(hitboxData.direction, hitboxData.pushForce, hitboxData.Hitter);
    }

    public void Push(Vector3 direction, int pushForce, GameObject hitter)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 dir = direction.normalized;
        if (hitter.transform.position.x > transform.position.x)
        {
            dir.x *= -1;
        }

        rb.AddForce(dir * pushForce / weight);
    }
    // TODO make this work?
    /*     public void Push(HitboxData hitboxData)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 dir = hitboxData.Direction.normalized;
            int pForce = hitboxData.PushForce;
    
            rb.AddForce(dir * pForce / weight);
        } */
}
