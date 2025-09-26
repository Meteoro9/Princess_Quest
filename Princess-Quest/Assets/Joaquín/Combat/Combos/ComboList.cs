using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboList", menuName = "Princess Quest /Combo List")]
public class ComboList : ScriptableObject
{
    [SerializeField]
    List<ComboSO> list;
}
