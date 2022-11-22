using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySpawner : MonoBehaviour
{
    [SerializeField] List<EnemySpawner> enemySpawners;
    [SerializeField] PowerUpPedestal pedastalDelayer;
    [SerializeField] Key keyDelayer;

    //NOTE DOES NOT ACCOUNT FOR IF BOTH PEDASTAL DELAYER AND KEY DELAYER ARE BOTH SET
    private void OnTriggerEnter2D(Collider2D collision){
        if(keyDelayer != null){
            Debug.Log(keyDelayer.isCollected);
        }


        if (collision.tag == "Player" && pedastalDelayer == null && keyDelayer == null){
                if(enemySpawners.Count != 0)
                {
                    foreach (var enemy in enemySpawners) 
                    {   
                        //Prevents double spawnning or enemy respawning
                        if (enemy.isDead != true && enemy.isSpawned != true)
                        {
                            enemy.SpawnEnemy();
                            enemy.isSpawned = true;
                        }
                    }
                } 
        }
        else if (collision.tag == "Player" && keyDelayer == null && pedastalDelayer.isCollected == true){
                if(enemySpawners.Count != 0)
                {
                    foreach (var enemy in enemySpawners) 
                    {   
                        //Prevents double spawnning or enemy respawning
                        if (enemy.isDead != true && enemy.isSpawned != true)
                        {
                            enemy.SpawnEnemy();
                            enemy.isSpawned = true;
                        }
                    }
                } 
        }
        else if (collision.tag == "Player" && pedastalDelayer == null && keyDelayer.isCollected == true){
                if(enemySpawners.Count != 0)
                {
                    foreach (var enemy in enemySpawners) 
                    {   
                        //Prevents double spawnning or enemy respawning
                        if (enemy.isDead != true && enemy.isSpawned != true)
                        {
                            enemy.SpawnEnemy();
                            enemy.isSpawned = true;
                        }
                    }
                } 
        }
    }
}
