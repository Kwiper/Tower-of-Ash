using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSword : MonoBehaviour
{
    [SerializeField]
    CombatData playerCombatData;
    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    UpgradeData upgradeData;

    [SerializeField]
    TextMeshProUGUI tinderText;

    [SerializeField]
    UISoundHandler soundHandler;

    private void Update()
    {
        if (upgradeData.swordUpgradeCount < upgradeData.swordMaxUpgrades)
        {
            tinderText.text = "Tinder Cost: " + upgradeData.swordTinderCosts[upgradeData.swordUpgradeCount];
        }
        else
        {
            tinderText.text = "MAX";
        }
    }

    public void UpgradeDamageMultiplier()
    {
        int i = upgradeData.swordUpgradeCount;

        int tinderCost = upgradeData.swordTinderCosts[i];

        int playerTinder = playerData.tinder;

        if (upgradeData.swordUpgradeCount < upgradeData.swordMaxUpgrades)
        {
            if (playerTinder >= tinderCost)
            {
                soundHandler.PlayMenuConfirmSound();
                playerData.tinder -= tinderCost;
                playerCombatData.damageMultiplier *= upgradeData.swordUpgradeMultiplier;
                upgradeData.swordUpgradeCount += 1;
            }
            else
            {
                soundHandler.PlayMenuIncorrectSound();
            }
        }
    }
}
