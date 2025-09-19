using System.Collections;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField]
    GameObject hitboxPrefab;

    [SerializeField]
    AttackSO attackLight;

    [SerializeField]
    AttackSO attackHeavy;

    bool isAttacking;

    public delegate AttackSO OnAttack(AttackType attack);
    public OnAttack onAttack;

    public void Attack(AttackType attackType)
    {
        if (!isAttacking)
        {
            AttackSO attack = onAttack?.Invoke(attackType) ?? GetBaseAttack(attackType);
            StartCoroutine(AttackCor(attack));
        }
    }

    IEnumerator AttackCor(AttackSO attack)
    {
        isAttacking = true;
        GameObject newHitbox = Instantiate(hitboxPrefab, transform);
        SetHitboxProperties(newHitbox, attack);

        newHitbox.SetActive(false);
        yield return new WaitForSeconds(attack.startUpTime);
        newHitbox.SetActive(true);
        yield return new WaitForSeconds(attack.activeTime);
        newHitbox.SetActive(false);
        yield return new WaitForSeconds(attack.endingTime);

        Destroy(newHitbox);
        isAttacking = false;
    }

    // TODO multiply offset.x * -1 if last movement was left, +1 if last movement was right
    void SetHitboxProperties(GameObject hitbox, AttackSO attack)
    {
        BoxCollider hitboxCollider = hitbox.GetComponent<BoxCollider>();
        hitboxCollider.size = attack.size;
        hitboxCollider.center = attack.offset;
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
}
