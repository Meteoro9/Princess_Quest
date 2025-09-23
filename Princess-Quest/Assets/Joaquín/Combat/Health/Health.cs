using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHurtbox
{
    [SerializeField]
    UnityEvent onDeathEvent = new();

    [SerializeField]
    int maxHp = 10;

    [SerializeField]
    int hp;
    int HP
    {
        get { return hp; }
        set { hp = Mathf.Clamp(value, 0, maxHp); }
    }

    void Awake()
    {
        HP = maxHp;
    }

    public void OnHurtboxHit(HitboxData hitboxData)
    {
        TakeDamage(hitboxData.Damage);
    }

    public void TakeDamage(int dmg)
    {
        if (HP - dmg <= 0)
        {
            OnDeath();
            return;
        }
        HP -= dmg;
    }

    void OnDeath()
    {
        onDeathEvent?.Invoke();
    }
}
