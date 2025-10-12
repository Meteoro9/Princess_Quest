using UnityEngine;

public class CombatInput : MonoBehaviour
{
    [SerializeField]
    KeyCode attackKey = KeyCode.Z;

    [SerializeField]
    KeyCode strongAttackKey = KeyCode.X;

    AttackComponent combatTest;
    Animator animator;

    void OnEnable()
    {
        combatTest = GetComponent<AttackComponent>();

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            combatTest.Attack(AttackType.Light);
            animator.SetBool("Punch", true);
        }
        else if (Input.GetKeyDown(strongAttackKey))
        {
            combatTest.Attack(AttackType.Heavy);
            animator.SetBool("Kick", true);
        }
        else
        {
            animator.SetBool("Punch", false);
            animator.SetBool("Kick", false);
        }
    }
}
