using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newUpgradeData", menuName = "Data/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    [Header("Sword upgrades")]
    public int swordUpgradeCount = 0;
    public int swordMaxUpgrades = 5;

    public float swordUpgradeMultiplier = 1.25f;

    public int[] swordTinderCosts = new int[] {10,25,50,75,120};

    [Header("Flame upgrades")]
    public int flameUpgradeCount = 0;
    public int flameMaxUpgrades = 5;

    public float flameTimeMultiplier = 2f;

    public int[] flameTinderCosts = new int[] {20,40,65,100,150};

    [Header("Flask upgrades")]
    public int flaskUpgradeCount = 0;
    public int flaskMaxUpgrades = 5;

    public int[] flaskTinderCosts = new int[] {50,100,175,250,500};

}
