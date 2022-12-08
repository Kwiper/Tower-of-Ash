using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class Portal : MonoBehaviour
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] DestinationIdentifier destinationPortal;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float range;
    [SerializeField] Transform rangePoint;

    [SerializeField]
    TextMeshPro text;

    [SerializeField]
    bool CanBeInteractedWith;

    Animator anim;

    GameObject newWorldBound;
    private bool isTeleporting = false;

    GameObject play;
    Player player;
    private void Awake()
    {
        play =  GameObject.FindGameObjectsWithTag("Player")[0];
        
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        anim.SetBool("open", false);
    }
    public void OnPlayerTriggered(Player play)
    {
        //play = FindObjectOfType<Player>();
        //this.play = play;
        //Debug.Log("Player entered portal");
        StartCoroutine(SwitchScene());
    }

    private void Update()
    {
        if (CheckIfPlayerInRange() && CanBeInteractedWith)
        {
            text.gameObject.SetActive(true);

            if (player.InputHandler.InteractInput && player.CheckIfGrounded())
            {
                player.InputHandler.UseInteractInput();
                anim.SetBool("open", true);
            }
        }
        else
        {
            text.gameObject.SetActive(false);
        }

        if (player.gameObject.GetComponent<PlayerInput>().currentControlScheme == "Keyboard")
        {
            text.text = "Press E to interact";
        }
        else if (player.gameObject.GetComponent<PlayerInput>().currentControlScheme == "Gamepad")
        {
            text.text = "Press B to interact";
        }
    }

    public void AnimationTrigger()
    {
        OnPlayerTriggered(player);
    }

    private bool CheckIfPlayerInRange()
    {
        return Physics2D.OverlapCircle(rangePoint.position, range, playerLayer);
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
        
        player.setConfiner(newWorldBound.GetComponent<Collider2D>());

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
