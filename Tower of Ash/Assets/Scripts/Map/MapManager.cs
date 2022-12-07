using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    Player player;
    [SerializeField]
    PlayerData playerData;

    [SerializeField]
    GameObject[] children;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (player.InputHandler.MapInput && playerData.unlockedMap)
            {

                foreach (GameObject child in children)
                {
                    child.SetActive(true);
                }

            }
            else
            {

                foreach (GameObject child in children)
                {
                    child.SetActive(false);
                }

            }
        }
    }


}
