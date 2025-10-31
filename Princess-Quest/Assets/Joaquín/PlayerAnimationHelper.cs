using UnityEngine;

public class PlayerAnimationHelper : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    AttackComponent attackComponent;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        attackComponent.AttackStarted += HandleAttackStart;
        attackComponent.AttackEnded += HandleAttackEnd;
    }

    void HandleAttackStart(AttackSO attackSO)
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

    void HandleAttackEnd(AttackSO attackSO)
    {
        if (attackSO.type == AttackType.Light)
        {
            animator.SetBool("Punch", false);
        }
        else if (attackSO.type == AttackType.Heavy)
        {
            animator.SetBool("Kick", false);
        }
    }
}
