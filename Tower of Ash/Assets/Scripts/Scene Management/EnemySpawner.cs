using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner: MonoBehaviour
{
    [SerializeField] 
    private GameObject enemyPrefab;
    [SerializeField] 
    private float rotationAngle = 0;
    private GameObject enemy;
    public bool isDead;
    public bool isSpawned = false;
    private Transform pos;

    public void SpawnEnemy(){
        var rot = new Vector3(0,0,rotationAngle);
        pos = GetComponent<Transform>();
        pos.position = new Vector3(pos.position.x, pos.position.y, -1);
        enemy = Instantiate(enemyPrefab, pos.position, Quaternion.identity);
        enemy.GetComponent<Transform>().Rotate(rot, Space.Self);
        //enemy.Transform.Rotate(rot, Space.Self);
    }

    public void DespawnEnemy(){
        Destroy(enemy);
    }

    private void Update()
    {
        if(isSpawned && enemy == null){
            isDead = true;
        }
    }

}
