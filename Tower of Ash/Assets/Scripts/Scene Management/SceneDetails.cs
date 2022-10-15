using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDetails : MonoBehaviour
{
    [SerializeField] List<SceneDetails> connectedScenes;
    public bool IsLoaded = false;


    //GameObject Play;
    
    Player Player;
    GameObject Play;
    private void Start()
    {
        Play =  GameObject.FindGameObjectsWithTag("Player")[0];
        Player = Play.GetComponent<Player>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player"){

            Debug.Log($"Entered {gameObject.name}");
            LoadScene();
            Player.SetCurrentScene(this);

            //Load all connected scenes
            foreach (var scene in connectedScenes)
            {
                scene.LoadScene();
            }
            
            //Unload the scenes that are no longer connected
            if (Player.PrevScene != null){
                var previouslyLoadedScenes = Player.PrevScene.connectedScenes;

                foreach (var scene in previouslyLoadedScenes)
                {
                    if(!connectedScenes.Contains(scene) && scene != this){
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
