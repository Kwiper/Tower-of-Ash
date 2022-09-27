using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAttackData", menuName = "Data/Attack Data")]

public class AttackData : ScriptableObject
{
    [Header("Melee Values")]
    public int damage = 5;
    public float damageMultiplier = 1;

    [Header("Projectile Values")]
    public int projectileDamage = 15;
}
