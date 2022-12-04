using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private int keyIdentifier = 0;
    private bool isOpen = false;
    private Key doorKey;
    private Tilemap Tilemapper;
    //TMRenderer.color = Color.gray;

    // Start is called before the first frame update
    void Start()
    {
        Tilemapper = GetComponent<Tilemap>();
        for (int i = 0; i < playerData.keys.Count; i++) 
        {
            //doorKey = doorKeyObj.GetComponent<Key>();
            if (playerData.keys[i] == keyIdentifier && playerData.keysCollected[i] == true)
            {	
	            GetComponent<CompositeCollider2D>().enabled = false;
                Tilemapper.color = Color.gray;

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
	                GetComponent<CompositeCollider2D>().enabled = false;
                    Tilemapper.color = Color.gray;                    
                    isOpen = true;
                } 
            }
        }
    }
}
