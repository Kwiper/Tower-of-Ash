using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCombatData", menuName = "Data/Combat Data")]

public class CombatData : ScriptableObject
{
    [Header("Melee Values")]
    public int damage = 5;
    public float damageMultiplier = 1;

    [Header("Projectile Values")]
    public int projectileDamage = 15;
    public int projectileSpeed = 20;



    public void IncreaseDamageMultiplier()
    {
        damageMultiplier *= 1.25f;
    }


}
