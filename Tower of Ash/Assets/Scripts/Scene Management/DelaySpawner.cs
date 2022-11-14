using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySpawner : MonoBehaviour
{
    [SerializeField] List<EnemySpawner> enemySpawners;

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player"){
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
