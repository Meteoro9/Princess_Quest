using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Hitbox : MonoBehaviour
{
    public delegate void HitEvent();
    public HitEvent OnHitboxHit;
    public HitboxData hitboxData;

    BoxCollider boxCollider;

    void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        hitboxData ??= new HitboxData(gameObject, 1);
    }

    void OnTriggerEnter(Collider other)
    {
        Hurtbox otherHurbox = other.GetComponent<Hurtbox>();
        if (otherHurbox != null)
        {
            // Debug.Log(transform.parent.gameObject.name + " has hit " + other.gameObject.name);
            otherHurbox.OnHit(hitboxData);
            OnHitboxHit?.Invoke();
        }
    }

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        boxCollider = GetComponent<BoxCollider>();
        Gizmos.color = Color.red;
        Vector3 actualSize = new Vector3(
            boxCollider.size.x * transform.parent.localScale.x,
            boxCollider.size.y * transform.parent.localScale.y,
            boxCollider.size.z * transform.parent.localScale.z
        );
        if (transform.rotation.y == 0)
        {
            Gizmos.DrawWireCube(transform.position + boxCollider.center, actualSize);
        }
        else
        {
            Vector3 rotColl = new Vector3(boxCollider.center.x * -1, boxCollider.center.y);
            Gizmos.DrawWireCube(transform.position + rotColl, actualSize);
        }
    }
#endif
}
