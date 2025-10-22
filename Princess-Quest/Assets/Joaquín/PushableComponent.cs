using UnityEngine;

public class PushableComponent : MonoBehaviour, IHurtbox
{
    [SerializeField]
    Rigidbody rigidBody;

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
        Vector3 dir = direction.normalized;
        if (hitter.transform.position.x > transform.position.x)
        {
            dir.x *= -1;
        }

        rigidBody.AddForce(dir * pushForce / weight);
    }
}
