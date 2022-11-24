using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCheckpoint : MonoBehaviour
{
    private Transform spawnPoint;
    public bool playerHasTriggered = false;

    private void Start()
    {
        spawnPoint  = this.gameObject.transform.GetChild(0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //var player = collision.gameObject.GetComponent<Player>();
        if(collision.gameObject.tag == "Player"){
            var player = collision.gameObject.GetComponent<Player>();
            player.setCheckPointPos(new Vector2(spawnPoint.position.x,spawnPoint.position.y));
            playerHasTriggered = true;

        }

    }
}
