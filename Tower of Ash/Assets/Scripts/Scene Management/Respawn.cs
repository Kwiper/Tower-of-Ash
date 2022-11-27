using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;

    public void RespawnScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

        playerData.healCharges = playerData.maxHealCharges;
    }

}
