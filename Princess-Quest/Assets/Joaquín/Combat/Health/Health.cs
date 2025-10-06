using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHurtbox
{
    [SerializeField]
    bool DestroyOnDeath = true;

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

    [SerializeField]
    bool isHurtboxEventActive = true;
    public bool IHurtboxActive { get; set; }

    void Awake()
    {
        HP = maxHp;
        IHurtboxActive = isHurtboxEventActive;
    }

    public void OnHurtboxHit(HitboxData hitboxData)
    {
        if (IHurtboxActive)
        {
            TakeDamage(hitboxData.damage);
        }
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
        if (DestroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
}
