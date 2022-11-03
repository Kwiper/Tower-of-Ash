using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeTimer : MonoBehaviour
{

    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    UpgradeData upgradeData;

    [SerializeField]
    TextMeshProUGUI tinderText;

    void Update()
    {
        if (upgradeData.flameUpgradeCount < upgradeData.flameMaxUpgrades)
        {
            tinderText.text = "Tinder Cost: " + upgradeData.flameTinderCosts[upgradeData.flameUpgradeCount];
        }
        else
        {
            tinderText.text = "MAX";
        }
    }

    public void UpgradePlayerTimer()
    {
        int i = upgradeData.flameUpgradeCount;

        int tinderCost = upgradeData.flameTinderCosts[i];

        int playerTinder = playerData.tinder;

        if (upgradeData.flameUpgradeCount < upgradeData.flameMaxUpgrades)
        {
            if (playerTinder >= tinderCost)
            {
                playerData.tinder -= tinderCost;
                playerData.timeReduction *= upgradeData.flameTimeMultiplier;
                upgradeData.flameUpgradeCount += 1;
            }
        }
    }
}
