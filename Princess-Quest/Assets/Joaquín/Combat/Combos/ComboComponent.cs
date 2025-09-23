using UnityEngine;

public class ComboComponent : MonoBehaviour
{
    [SerializeField]
    ComboSO comboList;

    int index = 0;

    void OnEnable()
    {
        AttackComponent atkComp = GetComponent<AttackComponent>();
        atkComp.onAttack += OnAttack;
        index = 0;
    }

    void OnDisable()
    {
        AttackComponent atkComp = GetComponent<AttackComponent>();
        atkComp.onAttack -= OnAttack;
    }

    AttackSO OnAttack(AttackType attack)
    {
        if (comboList.attacks.Count > index && comboList.attacks?[index].type == attack)
        {
            index++;
            return comboList.attacks[index - 1];
        }
        else
        {
            index = 0;
            return null;
        }
    }
}
