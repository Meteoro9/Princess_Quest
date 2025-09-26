using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnHurtboxHit = new();

    public void OnHit(HitboxData hitboxData)
    {
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
