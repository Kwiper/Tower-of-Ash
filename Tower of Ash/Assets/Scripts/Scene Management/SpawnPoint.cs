using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    GameObject play;
    private Transform playerPos;
    [SerializeField] Transform spawnPoint;
    private Rigidbody2D RB;
    private Player player;
    private Transform[] transforms;

    private void Awake()
    {
        play = GameObject.FindGameObjectsWithTag("EssentialObjects")[0];
        transforms = play.GetComponentsInChildren<Transform>();
        playerPos = play.GetComponent<Transform>();
        playerPos.position = spawnPoint.position;
        //Should relocate player's transform currently does not due to null pointer exception
        transforms[1].position = spawnPoint.position;


        var essentialClean =  GameObject.FindGameObjectsWithTag("EssentialObjects");
        //var RB;
        //Prevents players from duping themselves in the spawn room
        if(essentialClean.Length > 1){
            
            for (int i = 0; i < essentialClean.Length; i++) 
            {
                RB = essentialClean[i].GetComponentInChildren(typeof(Rigidbody2D)) as Rigidbody2D;
                //Freeze rotation, toggles the z-constraint boolean but removes the x and y constraint boolean
                RB.constraints = RigidbodyConstraints2D.FreezeRotation;
                player = essentialClean[i].GetComponentInChildren(typeof(Player)) as Player;
                
                var pos = essentialClean[i].GetComponent<Transform>().position;
                essentialClean[i].GetComponent<Transform>().position = new Vector2(0,0);
                player.respawnPosition();
                //If the player is the original, if not destroy it
                if(player.isReal == false){
                    Destroy(essentialClean[i]);
                }
                player.healthCanCountdown = true;
            }   
        }
    }
}
