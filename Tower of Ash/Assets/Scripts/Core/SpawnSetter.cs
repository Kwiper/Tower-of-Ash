using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSetter : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnPoint;
    [SerializeField]
    private PlayerData playerData;
    private Player player;
    private bool hasTriggered = false;
    [SerializeField]
    private LayerMask playerMask;



    private void OnTriggerEnter2D(Collider2D collision)
    {  

        if(collision.gameObject.tag == "Player" && collision.gameObject.layer == 8 && hasTriggered == false){

            player = collision.gameObject.GetComponent<Player>();

            player.setSpawnPosition(spawnPoint);
            //Added due to loading reasons
            playerData.spawnPoint = spawnPoint;
            
            hasTriggered = true;
        }

    }

}
