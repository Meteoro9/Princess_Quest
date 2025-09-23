using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Hitbox : MonoBehaviour
{
    UnityEvent OnHitboxHit = new();
    public HitboxData hitboxData;

    void OnEnable()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        hitboxData = hitboxData == null ? new(gameObject) : hitboxData;
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
}
