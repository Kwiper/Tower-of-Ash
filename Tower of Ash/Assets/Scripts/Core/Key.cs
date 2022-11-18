using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    public bool isCollected = false;
    [SerializeField]
    public int keyIdentifier = 0;   
    private int doorKey;
    private bool doorCollected;
    private bool inList = false;


    // Start is called before the first frame update
    void Start()
    {
        if (playerData.keys.Count == 0){
            playerData.keys.Add(this.keyIdentifier);
            playerData.keysCollected.Add(this.isCollected);
            //Debug.Log(doorKey);
        }
        else{
            for (int i = 0; i < playerData.keys.Count; i++) 
            {
                //doorKeyObj = playerData.keys[i];
                doorKey = playerData.keys[i];
                doorCollected = playerData.keysCollected[i];
                //Debug.Log(doorKey);
                if (doorKey == this.keyIdentifier)
                {
                    inList = true;
                    if(doorCollected == true){
	                    GetComponent<BoxCollider2D>().enabled = false;
	                    GetComponent<SpriteRenderer>().enabled = false;
                        isCollected = true;
                    }
                }
                else if (inList == false && i == (playerData.keys.Count-1)){
                    
                    playerData.keys.Add(this.keyIdentifier);
                    playerData.keysCollected.Add(this.isCollected);
                }
            }
        }
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {	
	        GetComponent<BoxCollider2D>().enabled = false;
	        GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < playerData.keys.Count; i++) 
            {
                doorKey = playerData.keys[i];

                if (doorKey == this.keyIdentifier)
                {
                    playerData.keysCollected[i] = true;
                }
            }
        }    
    }

}
