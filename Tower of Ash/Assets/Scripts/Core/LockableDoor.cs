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

    private BoxCollider2D boxCollider;

    private void Start(){
        Tilemapper = GetComponent<Tilemap>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {  

        if(collision.gameObject.tag == "Player" && collision.gameObject.layer == 8 && hasTriggered == false){

            hasTriggered = true;
	        tileCollide.enabled = true;
            Tilemapper.color = new Color(1,1,1,1);
            boxCollider.enabled = false;
        }

        if (!hasTriggered)
        {
            tileCollide.enabled = false;
            Tilemapper.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }

    }
}
