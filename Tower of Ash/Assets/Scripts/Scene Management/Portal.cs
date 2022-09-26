using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IplayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    public void OnPlayerTriggered(Player play)
    {
        //Debug.Log("Player entered portal");
        StartCoroutine(SwitchScene());
    }
    
    IEnumerator SwitchScene()
    {
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
