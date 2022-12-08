using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private DataLoadingManager loader;
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private PlayerBaseData basicData;
    [SerializeField]
    private CombatData combatData;
    [SerializeField]
    private UpgradeData upgradeData;
    [SerializeField]
    public GameObject Credits;
    [SerializeField]
    public GameObject mainMenu;
    private void Start()
    {
    }

    IEnumerator LoadGameScene()
    {
        //if(isTeleporting == false){
            //isTeleporting = true;
        DontDestroyOnLoad(gameObject);

        yield return SceneManager.LoadSceneAsync("Gameplay");
        loader = FindObjectOfType<DataLoadingManager>();
        loader.loadGame();
        Destroy(gameObject);
        //}
    }

    IEnumerator LoadNewGame()
    {
        yield return SceneManager.LoadSceneAsync("OpeningCutscene");
    }

    public void Resume()
    {
        //Time.timeScale = 1f;
        //GameIsPaused = false;
        //pauseMenuUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    //public void MainMenu()
    //{
        // Return to main menu\
    //    player.saveGame();
    //}

    public void Continue()
    {
        StartCoroutine(LoadGameScene());
        //player.saveGame();
    }

    public void Begin()
    {
        reset();
        StartCoroutine(LoadNewGame());
        //player.saveGame();
    }

    private void reset(){




            playerData.spawnPoint = new Vector2(-2,-58);
            //playerData.spawnPoint = new Vector2(1,1);

            upgradeData.swordUpgradeCount = 0;
            upgradeData.flameUpgradeCount = 0;
            upgradeData.flaskUpgradeCount = 0;

            combatData.damageMultiplier = 1;


            //PlayerData


            playerData.unlockedDoubleJump = false;
            playerData.amountOfJumps = 1;

            playerData.unlockedFireball = false;    
            playerData.unlockedWallJump = false;
            playerData.unlockedDash = false;
            playerData.unlockedChargeAttack = false;

            playerData.unlockedHealing = false;
            playerData.maxHealCharges = 3;

            playerData.unlockedMap = false;

            playerData.movementTutorial = false;
            playerData.combatTutorial = false;
            playerData.lookTutorial = false;

            playerData.tinder = 0;
            playerData.timeReduction = basicData.timeReduction;

            for (int i = 0; i < playerData.keysCollected.Count; i++) 
            {
                playerData.keysCollected[i] = false;
            }

            playerData.CollectedTinderCacheLocations.Clear();
            playerData.ids.Clear();
            


    }


    public void back(){
        mainMenu.SetActive(true);
        Credits.SetActive(false);
    }

    public void toCredits(){
        mainMenu.SetActive(false);
        Credits.SetActive(true);
    }
}

