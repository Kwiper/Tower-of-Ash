using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Serializable;
[System.Serializable]
public class SaveDataManager
{
    #region Player Data Variables

    
    public bool unlockedDJ;
    public int numberofJumps;

    public bool unlockedFB;    
    public bool unlockedWJ;
    public bool unlockedDS;
    public bool unlockedCA;
    public bool unlockedHE;

    public int healingFlaskMax;

    public bool[] keysCollected;
    public bool mapUnlocked;
    public int[] mapId;

    public bool tutorialMove;
    public bool tutorialCombat;
    public bool tutorialLook;

    public int tinder;

    public float[][] tinderCacheLoc;

    #endregion

    #region Player Values
    public float[] spawnPoint;
    #endregion

    #region Upgrade Screen
    public int swordUpgradeCount;
    public int flameUpgradeCount;
    public int flaskUpgradeCount;    
    #endregion

    #region CombatUpgrades
    public float damageMultiplier;
  
    #endregion 

    public SaveDataManager (PlayerData playerData, CombatData combatData, UpgradeData upgradeData){
        //Player Data
        keysCollected = new bool[playerData.keysCollected.Count];
        mapId = new int[playerData.ids.Count];
        spawnPoint = new float[2];
        tinderCacheLoc = new float[29][];

        unlockedDJ = playerData.unlockedDoubleJump;
        numberofJumps = playerData.amountOfJumps;

        unlockedFB = playerData.unlockedFireball;    
        unlockedWJ = playerData.unlockedWallJump;
        unlockedDS = playerData.unlockedDash;
        unlockedCA = playerData.unlockedChargeAttack;
        unlockedHE = playerData.unlockedHealing;

        tinder = playerData.tinder;

        healingFlaskMax = playerData.maxHealCharges;
        for (int i = 0; i < playerData.keysCollected.Count; i++) 
        {
            
            keysCollected[i] =  playerData.keysCollected[i];
        }
        
        mapUnlocked = playerData.unlockedMap;
        for (int i = 0; i < playerData.ids.Count; i++) 
        {
            mapId[i] =  playerData.ids[i];
        }        


        tutorialMove =  playerData.movementTutorial;
        tutorialCombat = playerData.combatTutorial;
        tutorialLook = playerData.lookTutorial;

        if (playerData.CollectedTinderCacheLocations.Count != 0){
            for (int i = 0; i < playerData.CollectedTinderCacheLocations.Count; i++) 
            {
                tinderCacheLoc[i] =  new float[] {playerData.CollectedTinderCacheLocations[i].x , playerData.CollectedTinderCacheLocations[i].y};
                Debug.Log(tinderCacheLoc.Length);
                Debug.Log(tinderCacheLoc[0].Length);
            }
        }


        //Values stored on Player
        
        spawnPoint[0] = playerData.spawnPoint.x;
        spawnPoint[1] = playerData.spawnPoint.y;

        //Values stored on Upgrade Screen
        swordUpgradeCount = upgradeData.swordUpgradeCount;
        flameUpgradeCount = upgradeData.flameUpgradeCount;
        flaskUpgradeCount = upgradeData.flaskUpgradeCount;    


        //Values stored on CombatData
        damageMultiplier = combatData.damageMultiplier;

    }      
}
