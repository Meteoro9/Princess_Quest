using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnHurtboxHit = new();

    public void OnHit(HitboxData hitboxData)
    {
        // Debug.Log(gameObject.name + " was hit ");

        if (hitboxData != null)
        {
            foreach (IHurtbox item in GetComponents<IHurtbox>())
            {
                item.OnHurtboxHit(hitboxData);
            }
        }
        else
        {
            Debug.LogWarning("HitboxData missing");
        }
        OnHurtboxHit?.Invoke();
    }
}
