using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadHelper : MonoBehaviour
{
  private DataLoadingManager loader;


    IEnumerator LoadGameScene()
    {
        //if(isTeleporting == false){
            //isTeleporting = true;
        DontDestroyOnLoad(gameObject);

        yield return SceneManager.LoadSceneAsync("Gameplay");
        //loader = FindObjectOfType<DataLoadingManager>();
        //loader.loadGame();
        Destroy(gameObject);
        
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameScene());

    }
}
