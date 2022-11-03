using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner: MonoBehaviour
{
    [SerializeField] 
    private GameObject enemyPrefab;
    private GameObject enemy;
    public bool isDead;
    public bool isSpawned = false;
    private Transform pos;

    public void SpawnEnemy(){
        pos = GetComponent<Transform>();
        enemy = Instantiate(enemyPrefab, pos.position, Quaternion.identity);
    }

    public void DespawnEnemy(){
        Destroy(enemy);
    }

    private void Update()
    {
        if(enemy.GetComponent<Entity>().Health <= 0){
            isDead = true;
        }
    }

}
