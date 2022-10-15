using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EssentialObjects : MonoBehaviour
{
    private void Awake(){
        
        DontDestroyOnLoad(gameObject);
        //Debug.Log("I keep from destroy");
        

    }
    
/*
    void Update(){
        if(SceneManager.GetActiveScene().name == "Upgrade"){
            //gameObject.SetActive(false);

            foreach (Transform child in transform){
                child.gameObject.SetActive(false);
            }
            //Destroy(gameObject);  
        }
        else{
            foreach (Transform child in transform){
                child.gameObject.SetActive(true);
            }
       }
    }
    */
}
