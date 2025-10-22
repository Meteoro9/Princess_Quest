using UnityEngine;

public class CombatInput : MonoBehaviour
{
    AttackComponent combatTest;
    Animator animator;

    void OnEnable()
    {
        combatTest = GetComponent<AttackComponent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Light Attack"))
        {
            combatTest.Attack(AttackType.Light);
            animator.SetBool("Punch", true);
        }
        else if (Input.GetButtonDown("Heavy Attack"))
        {
            combatTest.Attack(AttackType.Heavy);
            animator.SetBool("Kick", true);
        }
        else
        {
            animator.SetBool("Punch", false);
            animator.SetBool("Kick", false);
        }
        Input.GetButtonDown("Fire1");
    }
}
