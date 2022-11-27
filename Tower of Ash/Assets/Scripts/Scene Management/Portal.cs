using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IplayerTriggerable
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] DestinationIdentifier destinationPortal;
    [SerializeField] Transform spawnPoint;
    GameObject newWorldBound;
    private bool isTeleporting = false;

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
        if(isTeleporting == false){
            isTeleporting = true;
        DontDestroyOnLoad(gameObject);

        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        var destPortal = FindObjectOfType<Portal>();
        var roomPortals = FindObjectsOfType<Portal>();
        newWorldBound = GameObject.Find("WorldBoundary");
        //Debug.Log(newWorldBound);
        for (int i = 0; i < roomPortals.Length; i++) 
        {
            if (roomPortals[i] != this && roomPortals[i].destinationPortal == this.destinationPortal){
            destPortal = roomPortals[i];
            break;
            }
        }
        var playTransform = play.GetComponent<Transform>();
        var playa = play.GetComponent<Player>();
        playa.setConfiner(newWorldBound.GetComponent<Collider2D>());

        playTransform.position = destPortal.spawnPoint.position;
        //destPortal.setActive(false);
        var essentialClean = GameObject.FindGameObjectsWithTag("EssentialObjects");

        //Prevents players from duping themselves in the spawn room
        if(essentialClean.Length > 1){
            Destroy(essentialClean[1]);
        }

        Destroy(gameObject);
        }
    }

    public enum DestinationIdentifier{A,B,C,D,E}

    public Transform SpawnPoint => spawnPoint;
}
