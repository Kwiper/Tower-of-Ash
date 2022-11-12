using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private int keyIdentifier = 0;
    private bool isOpen = false;
    private Key doorKey;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < playerData.keys.Count; i++) 
        {
            //doorKey = doorKeyObj.GetComponent<Key>();
            if (playerData.keys[i] == keyIdentifier && playerData.keysCollected[i] == true)
            {	
	            GetComponent<BoxCollider2D>().enabled = false;
	            GetComponent<SpriteRenderer>().enabled = false;
                isOpen = true;
            } 
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen == false){
            for (int i = 0; i < playerData.keys.Count; i++) 
            {
                if (playerData.keys[i] == keyIdentifier && playerData.keysCollected[i] == true)
                {	
	                GetComponent<BoxCollider2D>().enabled = false;
	                GetComponent<SpriteRenderer>().enabled = false;
                    isOpen = true;
                } 
            }
        }
    }
}
