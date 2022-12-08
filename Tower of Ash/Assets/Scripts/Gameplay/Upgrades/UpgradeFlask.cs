using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeFlask : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    UpgradeData upgradeData;

    [SerializeField]
    TextMeshProUGUI tinderText;

    [SerializeField]
    UISoundHandler soundHandler;
    void Update()
    {
        if (upgradeData.flaskUpgradeCount < upgradeData.flaskMaxUpgrades)
        {
            tinderText.text = "Tinder Cost: " + upgradeData.flaskTinderCosts[upgradeData.flaskUpgradeCount];
        }
        else
        {
            tinderText.text = "MAX";
        }
    }

    public void UpgradePlayerFlasks()
    {
        int i = upgradeData.flaskUpgradeCount;

        int tinderCost = upgradeData.flaskTinderCosts[i];

        int playerTinder = playerData.tinder;

        if (upgradeData.flaskUpgradeCount < upgradeData.flaskMaxUpgrades)
        {
            if (playerTinder >= tinderCost)
            {
                soundHandler.PlayMenuConfirmSound();
                playerData.tinder -= tinderCost;
                playerData.maxHealCharges += 1;
                upgradeData.flaskUpgradeCount += 1;
            }
            else
            {
                soundHandler.PlayMenuIncorrectSound();
            }
        }
    }
}
