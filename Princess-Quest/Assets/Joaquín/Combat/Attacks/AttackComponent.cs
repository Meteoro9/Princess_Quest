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

    public delegate void AttackEvent(AttackSO attackSO);
    public event AttackEvent AttackStarted;
    public AttackEvent AttackEnded;

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
        // HandleAnimationStart(attack);
        AttackStarted?.Invoke(attack);
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
        AttackEnded?.Invoke(attack);
        // HandleAnimationEnd(attack);
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

    /*     void HandleAnimationStart(AttackSO attackSO)
        {
            if (attackSO.type == AttackType.Light)
            {
                animator.SetBool("Punch", true);
            }
            else if (attackSO.type == AttackType.Heavy)
            {
                animator.SetBool("Kick", true);
            }
        }
    
        void HandleAnimationEnd(AttackSO attackSO)
        {
            if (attackSO.type == AttackType.Light)
            {
                animator.SetBool("Punch", false);
            }
            else if (attackSO.type == AttackType.Heavy)
            {
                animator.SetBool("Kick", false);
            }
        } */
}
