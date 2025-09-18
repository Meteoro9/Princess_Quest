using UnityEngine;

public class CombatInput : MonoBehaviour
{
    [SerializeField]
    KeyCode attackKey = KeyCode.Z;

    [SerializeField]
    KeyCode strongAttackKey = KeyCode.X;

    AttackComponent combatTest;

    void OnEnable()
    {
        combatTest = GetComponent<AttackComponent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(attackKey))
        {
            combatTest.Attack(AttackType.Light);
        }
        else if (Input.GetKeyDown(strongAttackKey))
        {
            combatTest.Attack(AttackType.Heavy);
        }
    }
}
