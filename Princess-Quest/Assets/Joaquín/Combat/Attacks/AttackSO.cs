using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Princess Quest /Attack")]
public class AttackSO : ScriptableObject
{
    public AttackType type;

    public List<AttackType> comboPath = new();

    [Header("Attack data")]
    [Range(0, 5)]
    public float startUpTime = 1.0f;

    [Range(0, 10)]
    public float activeTime = 1.0f;

    [Range(0, 5)]
    public float endingTime = 1.0f;

    public int damage = 1;

    [Header("Hitbox data")]
    public Vector3 offset;
    public Vector3 size = new(1, 1, 1);
    public Vector3 direction;
}
