using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Hitbox : MonoBehaviour
{
    UnityEvent OnHitboxHit = new();

    void OnEnable()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Hurtbox otherHurbox = other.GetComponent<Hurtbox>();
        if (otherHurbox is not null)
        {
            // Debug.Log(transform.parent.gameObject.name + " has hit " + other.gameObject.name);
            otherHurbox.OnHit();
            OnHitboxHit?.Invoke();
        }
    }
}
