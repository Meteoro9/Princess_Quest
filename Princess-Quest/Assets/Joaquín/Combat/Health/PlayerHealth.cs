using System.Collections;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField]
    bool isInvincible = false;

    [SerializeField]
    float invinTime = 0.3f;

    public override void TakeDamage(int dmg)
    {
        if (isInvincible)
        {
            return;
        }
        base.TakeDamage(dmg);
        StartCoroutine(InvinCoroutine());
    }

    IEnumerator InvinCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invinTime);
        isInvincible = false;
    }
}
