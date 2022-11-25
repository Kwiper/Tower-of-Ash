using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinderCache : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private int tinderReward = 50;
    //private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {	
	        playerData.tinder = playerData.tinder + tinderReward;
            var pos = new Vector2(gameObject.GetComponent<Transform>().position.x,gameObject.GetComponent<Transform>().position.y);
            playerData.CollectedTinderCacheLocations.Add(pos);
	        GetComponent<BoxCollider2D>().enabled = false;
	        GetComponent<SpriteRenderer>().enabled = false;
        }    
    }

    public void setTinderReward(int tinderRewardVal){
        tinderReward = tinderRewardVal;
    }

    public void setPlayerData(PlayerData playData){
        playerData = playData;
    }   
}
