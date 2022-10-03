using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IplayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;

    GameObject play;
    private void Awake()
    {
        play =  GameObject.FindGameObjectsWithTag("Player")[0];
    }
    public void OnPlayerTriggered(Player play)
    {
        //play = FindObjectOfType<Player>();
        //this.play = play;
        //Debug.Log("Player entered portal");
        StartCoroutine(SwitchScene());
    }
    
    IEnumerator SwitchScene()
    {
        DontDestroyOnLoad(gameObject);

        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        var destPortal = FindObjectOfType<Portal>();
        var roomPortals = FindObjectsOfType<Portal>();
        for (int i = 0; i < roomPortals.Length; i++) 
        {
            if (roomPortals[i] != this){
            destPortal = this;
            break;
            }
        }
        var playTransform = play.GetComponent<Transform>();

        playTransform.position = destPortal.spawnPoint.position;

        Destroy(gameObject);
        Debug.Log("I happen");
    }


    public Transform SpawnPoint => spawnPoint;
}
