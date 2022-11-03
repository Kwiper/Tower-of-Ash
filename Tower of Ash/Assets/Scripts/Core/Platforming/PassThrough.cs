using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThrough : MonoBehaviour
{
    private new CompositeCollider2D collider;
    private bool playerOnPlatform;
    private GameObject player;

    private void Start(){
        //Finds collider for platform and finds player script on player gameobject
        collider = GetComponent<CompositeCollider2D>();
        var playerPossibles = GameObject.FindGameObjectsWithTag("Player");
        player = playerPossibles[0];
        
        var batsToAdd = GameObject.FindObjectsOfType<FireBat>();
 
        for (int i = 0; i < batsToAdd.Length; i++) 
        {
            Physics2D.IgnoreCollision(batsToAdd[i].GetComponent<BoxCollider2D>(), GetComponent<CompositeCollider2D>(), true);
        }

    }


    private void Update(){
        //Detects if player is on platform and is trying to leave by going down
        var playerControl = player.GetComponent<Player>();

        if (playerOnPlatform && playerControl.InputHandler.NormInputY < 0){
            //Only ignores collisions between player and platform so that other enemies don't fall through
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<CompositeCollider2D>(), true);
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider(){
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<CompositeCollider2D>(), false);
    }
    //Determines if player is standing on correct platform
    private void SetPlayerOnPlatform(Collision2D other, bool value){

        var playerCollide = other.gameObject.GetComponent<Player>();

        if (playerCollide != null)
        {
            playerOnPlatform = value;
        }
    }


    private void OnCollisionEnter2D(Collision2D other){
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other){
        SetPlayerOnPlatform(other, true);
    }

}
