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

    [SerializeField]
    Sprite[] sprites;

    SpriteRenderer sRenderer;

    private int doorKey;
    private bool doorCollected;
    private bool inList = false;


    // Start is called before the first frame update
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();

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
	                    sRenderer.sprite = sprites[1];
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

    private void Update()
    {
        if (isCollected)
        {
            sRenderer.sprite = sprites[1];
        }
        else
        {
            sRenderer.sprite = sprites[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {	
            isCollected = true;
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
