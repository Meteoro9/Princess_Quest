using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHurtbox
{
    [SerializeField]
    UnityEvent onDeathEvent = new();

    [SerializeField]
    int maxHp = 10;
    public int MaxHP
    {
        get { return maxHp; }
    }

    [SerializeField]
    int hp;
    public int HP
    {
        get { return hp; }
        private set { hp = Mathf.Clamp(value, 0, maxHp); }
    }

    void Awake()
    {
        HP = maxHp;
    }

    public void OnHurtboxHit(HitboxData hitboxData)
    {
        TakeDamage(hitboxData.damage);
    }

    public void TakeDamage(int dmg)
    {
        if (HP - dmg <= 0)
        {
            HP -= dmg;
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
