using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinderCache : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {	
	        //FindObjectOfType<OyxgenManager>().oxygenRegenPickup();
	        GetComponent<BoxCollider2D>().enabled = false;
	        GetComponent<SpriteRenderer>().enabled = false;
        }    
    }
}
