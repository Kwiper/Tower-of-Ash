using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LockableDoor : MonoBehaviour
{

    private Player player;
    public bool hasTriggered = false;
    private Tilemap Tilemapper;
    [SerializeField]
    private TilemapCollider2D tileCollide;

    private void start(){
        Tilemapper = GetComponent<Tilemap>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {  

        if(collision.gameObject.tag == "Player" && collision.gameObject.layer == 8 && hasTriggered == false){

            hasTriggered = true;
	        tileCollide.enabled = true;
            Tilemapper.color = Color.white;


        }

    }
}
