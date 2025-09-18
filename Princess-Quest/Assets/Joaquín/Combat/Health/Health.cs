using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    UnityEvent onDeathEvent = new();

    [SerializeField]
    int maxHp = 10;

    int hp;
    int HP
    {
        get { return hp; }
        set { hp = math.clamp(hp, 0, maxHp); }
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
