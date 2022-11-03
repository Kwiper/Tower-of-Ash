using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinderCache : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private int tinderReward = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {	
	        playerData.tinder = playerData.tinder + tinderReward;
	        GetComponent<BoxCollider2D>().enabled = false;
	        GetComponent<SpriteRenderer>().enabled = false;
        }    
    }
}
