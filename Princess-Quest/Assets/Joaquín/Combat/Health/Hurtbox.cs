using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]
public class Hurtbox : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnHurtboxHit = new();

    public void OnHit()
    {
        // Debug.Log(gameObject.name + " was hit ");
        OnHurtboxHit?.Invoke();
    }
}
