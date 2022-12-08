using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;

    public static bool ascendSelected;

    float blackTimer = 4.5f;
    float sceneChangeTimer = 2.87f;

    EventManager eventManager;
    private void Start()
    {
        eventManager = FindObjectOfType<EventManager>();

    }

    public void RespawnScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        ascendSelected = false;
        playerData.healCharges = playerData.maxHealCharges;
    }

    public void AscendButton()
    {
        ascendSelected = true;
        eventManager.TurnOffEventSystem();
    }

    private void Update()
    {
        if (ascendSelected)
        {
            blackTimer -= Time.deltaTime;
        }
        if (!ascendSelected)
        {
            blackTimer = 4.5f;
            sceneChangeTimer = 2.87f;
        }

        if(blackTimer <= 0)
        {
            eventManager.TurnScreenBlack();
            sceneChangeTimer -= Time.deltaTime;
        }

        if(sceneChangeTimer <= 0)
        {
            RespawnScene();
        }

    }



}
