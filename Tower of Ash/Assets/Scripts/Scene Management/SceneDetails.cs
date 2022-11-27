using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDetails : MonoBehaviour
{
    [SerializeField] List<SceneDetails> connectedScenes;
    [SerializeField] List<EnemySpawner> enemySpawners;
    public bool IsLoaded = false;


    //GameObject Play;
    
    Player Player;
    GameObject Play;
    private void Start()
    {
        Play =  GameObject.FindGameObjectsWithTag("Player")[0];
        Player = Play.GetComponent<Player>();
    }
    

    private void OnTriggerStay2D(Collider2D collision){
        if (collision.tag == "Player"){

            Debug.Log($"Entered {gameObject.name}");
            LoadScene();
                foreach (var enemy in enemySpawners) 
                {
                    //Prevents double spawnning or enemy respawning
                    if (enemy.isDead != true && enemy.isSpawned != true)
                    {
                        enemy.SpawnEnemy();
                        enemy.isSpawned = true;
                    }
                }            
            Player.SetCurrentScene(this);

            //Load all connected scenes
            foreach (var scene in connectedScenes)
            {
                scene.LoadScene();
                if(scene.enemySpawners.Count != 0)
                {
                    foreach (var enemy in scene.enemySpawners) 
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
            
            //Unload the scenes that are no longer connected
            if (Player.PrevScene != null){
                var previouslyLoadedScenes = Player.PrevScene.connectedScenes;

                foreach (var scene in previouslyLoadedScenes)
                {
                    if(!connectedScenes.Contains(scene) && scene != this){
                        //Unloads still alive enemies
                        foreach (var enemy in scene.enemySpawners) 
                        {
                            enemy.DespawnEnemy();
                            enemy.isSpawned = false;
                        }


                        scene.UnloadScene();
                    }
                }
            }
        }
    }

   


    public void LoadScene(){
            if(!IsLoaded){
                SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
                IsLoaded = true;
            }
    }

    public void UnloadScene(){
            
            if(IsLoaded){
                SceneManager.UnloadSceneAsync(gameObject.name);
                IsLoaded = false;
            }
    }    
}
