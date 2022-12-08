using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoadingManager : MonoBehaviour
{
    [SerializeField]
    private Player player;    
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private CombatData combatData;
    [SerializeField]
    private UpgradeData upgradeData;
    private List<Vector2> LoadedTinderCacheLocations;
    // Start is called before the first frame update
    void Start()
    {
        SaveDataManager data = SaveSystem.LoadPlayer();

        if(data != null){

            player.spawnPoint = new Vector2(data.spawnPoint[0],data.spawnPoint[1]);


            upgradeData.swordUpgradeCount = data.swordUpgradeCount;
            upgradeData.flameUpgradeCount = data.flameUpgradeCount;
            upgradeData.flaskUpgradeCount = data.flameUpgradeCount;

            combatData.damageMultiplier = data.damageMultiplier;


            //PlayerData


            playerData.unlockedDoubleJump = data.unlockedDJ;
            playerData.amountOfJumps = data.numberofJumps;

            playerData.unlockedFireball = data.unlockedFB;    
            playerData.unlockedWallJump = data.unlockedWJ;
            playerData.unlockedDash = data.unlockedDS;
            playerData.unlockedChargeAttack = data.unlockedCA;

            playerData.unlockedHealing = data.unlockedHE;
            playerData.maxHealCharges = data.healingFlaskMax;

            for (int i = 0; i < playerData.keysCollected.Count; i++) 
            {
                playerData.keysCollected[i] = data.keysCollected[i];
            }

            playerData.unlockedMap = data.mapUnlocked;

            playerData.movementTutorial = data.tutorialMove;
            playerData.combatTutorial = data.tutorialCombat;
            playerData.lookTutorial = data.tutorialLook;

            for (int i = 0; i < data.tinderCacheLoc.Length; i++){

                LoadedTinderCacheLocations.Add(new Vector2(data.tinderCacheLoc[i][0],data.tinderCacheLoc[i][1]));

            }

            foreach(Vector2 cacheLoc in LoadedTinderCacheLocations){
                for(int i = 0; i < playerData.CollectedTinderCacheLocations.Count; i++){
                    if(cacheLoc == playerData.CollectedTinderCacheLocations[i]){
                        break;
                    }
                    //If last value in array and still not equal to anything add to discovered
                    else if(cacheLoc != playerData.CollectedTinderCacheLocations[playerData.CollectedTinderCacheLocations.Count-1] && i == playerData.CollectedTinderCacheLocations.Count -1){
                        playerData.CollectedTinderCacheLocations.Add(cacheLoc);
                    }
                }
            }

        }


    }



    public void loadGame(){
        SaveDataManager data = SaveSystem.LoadPlayer();

        if(data != null){

            player.spawnPoint = new Vector2(data.spawnPoint[0],data.spawnPoint[1]);
            playerData.spawnPoint = new Vector2(data.spawnPoint[0],data.spawnPoint[1]);

            upgradeData.swordUpgradeCount = data.swordUpgradeCount;
            upgradeData.flameUpgradeCount = data.flameUpgradeCount;
            upgradeData.flaskUpgradeCount = data.flameUpgradeCount;

            combatData.damageMultiplier = data.damageMultiplier;


            //PlayerData


            playerData.unlockedDoubleJump = data.unlockedDJ;
            playerData.amountOfJumps = data.numberofJumps;

            playerData.unlockedFireball = data.unlockedFB;    
            playerData.unlockedWallJump = data.unlockedWJ;
            playerData.unlockedDash = data.unlockedDS;
            playerData.unlockedChargeAttack = data.unlockedCA;

            playerData.unlockedHealing = data.unlockedHE;
            playerData.maxHealCharges = data.healingFlaskMax;

            for (int i = 0; i < playerData.keysCollected.Count; i++) 
            {
                playerData.keysCollected[i] = data.keysCollected[i];
            }

            playerData.unlockedMap = data.mapUnlocked;

            playerData.movementTutorial = data.tutorialMove;
            playerData.combatTutorial = data.tutorialCombat;
            playerData.lookTutorial = data.tutorialLook;
            
            for (int i = 0; i < data.tinderCacheLoc.Length; i++){

                LoadedTinderCacheLocations.Add(new Vector2(data.tinderCacheLoc[i][0],data.tinderCacheLoc[i][1]));

            }

            foreach(Vector2 cacheLoc in LoadedTinderCacheLocations){
                for(int i = 0; i < playerData.CollectedTinderCacheLocations.Count; i++){
                    if(cacheLoc == playerData.CollectedTinderCacheLocations[i]){
                        break;
                    }
                    //If last value in array and still not equal to anything add to discovered
                    else if(cacheLoc != playerData.CollectedTinderCacheLocations[playerData.CollectedTinderCacheLocations.Count-1] && i == playerData.CollectedTinderCacheLocations.Count -1){
                        playerData.CollectedTinderCacheLocations.Add(cacheLoc);
                    }
                }
            }

        }


    }
    
}
