using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoadingManager : MonoBehaviour
{
    
    private Player player;    
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private CombatData combatData;
    [SerializeField]
    private UpgradeData upgradeData;

    private List<Vector2> LoadedTinderCacheLocations;
    private Vector2 test;


    IEnumerator waiter()
    {

        yield return new WaitForSecondsRealtime(1.5f);
        player = FindObjectOfType<Player>();
        LoadedTinderCacheLocations = new List<Vector2>();
    }

    private void Update()
    {
        /*
        if (player.InputHandler.LoadInput)
        {
            player.InputHandler.UseLoadInput();
            Debug.Log("Load input is being used");
            loadGame();
        }
        */
    }

    public void loadGame(){
        SaveDataManager data = SaveSystem.LoadPlayer();

        if(data != null){

            //player.spawnPoint = new Vector2(data.spawnPoint[0],data.spawnPoint[1]);
            //player.spawnPoint = new Vector2(1,1);
            playerData.spawnPoint = new Vector2(data.spawnPoint[0],data.spawnPoint[1]);
            //playerData.spawnPoint = new Vector2(1,1);

            upgradeData.swordUpgradeCount = data.swordUpgradeCount;
            upgradeData.flameUpgradeCount = data.flameUpgradeCount;
            upgradeData.flaskUpgradeCount = data.flameUpgradeCount;

            combatData.damageMultiplier = data.damageMultiplier;


            //PlayerData


            playerData.timeReduction = data.timeReduction;

            playerData.unlockedDoubleJump = data.unlockedDJ;
            playerData.amountOfJumps = data.numberofJumps;

            playerData.unlockedFireball = data.unlockedFB;    
            playerData.unlockedWallJump = data.unlockedWJ;
            playerData.unlockedDash = data.unlockedDS;
            playerData.unlockedChargeAttack = data.unlockedCA;

            playerData.unlockedHealing = data.unlockedHE;
            playerData.maxHealCharges = data.healingFlaskMax;

            playerData.unlockedMap = data.mapUnlocked;

            playerData.movementTutorial = data.tutorialMove;
            playerData.combatTutorial = data.tutorialCombat;
            playerData.lookTutorial = data.tutorialLook;

            playerData.tinder = data.tinder;

            for (int i = 0; i < playerData.keysCollected.Count; i++) 
            {
                playerData.keysCollected[i] = data.keysCollected[i];
            }

            for (int i = 0; i < data.tinderCacheID.Length; i++){

                    Debug.Log("This is test "+data.tinderCacheID);
                    playerData.CollectedTinderCacheID.Add(data.tinderCacheID[i]);

                    Debug.Log("I go after");
            }


            

        }


    }
    
}
