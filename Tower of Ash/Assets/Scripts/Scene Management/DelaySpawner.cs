using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySpawner : MonoBehaviour
{
    [SerializeField] List<EnemySpawner> enemySpawners;
    [SerializeField] PowerUpPedestal pedastalDelayer;
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player" && pedastalDelayer == null){
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
        else if (collision.tag == "Player" && pedastalDelayer.isCollected == true){
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
