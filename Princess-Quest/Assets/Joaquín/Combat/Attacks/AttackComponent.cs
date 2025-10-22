using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttackComponent : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnAttackHit = new();

    [SerializeField]
    GameObject hitboxPrefab;

    [SerializeField]
    AttackSO attackLight;

    [SerializeField]
    AttackSO attackHeavy;

    bool isAttacking;
    public bool IsAttacking => isAttacking;

    public void Attack(AttackType attackType)
    {
        if (!isAttacking)
        {
            AttackSO attack = GetBaseAttack(attackType);
            StartCoroutine(AttackCor(attack));
        }
    }

    IEnumerator AttackCor(AttackSO attack)
    {
        isAttacking = true;
        GameObject newHitbox = Instantiate(hitboxPrefab, transform);
        Hitbox hitbox = newHitbox.GetComponent<Hitbox>();
        hitbox.OnHitboxHit += InvokeOnAttackHit;
        HitboxData.Set(attack, newHitbox);

        newHitbox.SetActive(false);
        yield return new WaitForSeconds(attack.startUpTime);
        newHitbox.SetActive(true);
        yield return new WaitForSeconds(attack.activeTime);
        newHitbox.SetActive(false);
        yield return new WaitForSeconds(attack.endingTime);

        Destroy(newHitbox);
        isAttacking = false;
    }

    AttackSO GetBaseAttack(AttackType attackType)
    {
        return attackType switch
        {
            AttackType.Light => attackLight,
            AttackType.Heavy => attackHeavy,
            _ => throw new System.NotImplementedException(),
        };
    }

    void InvokeOnAttackHit()
    {
        OnAttackHit?.Invoke();
    }
}
