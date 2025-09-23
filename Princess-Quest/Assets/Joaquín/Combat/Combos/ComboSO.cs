using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo", menuName = "Princess Quest /Combo")]
public class ComboSO : ScriptableObject
{
    public List<AttackSO> attacks = new();
}
