using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPedestal : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private int upgrade;

    [SerializeField]
    GameObject text;
 
    [SerializeField]
    Sprite[] sprites;
    const int Dash = 1;
    const int WallJump = 2;
    const int Fireball = 3;
    const int ChargeAttack = 4;
    const int DoubleJump = 5;
    const int Healing = 6;

    public bool isCollected = false;

    SpriteRenderer sRenderer;

    private void Start()
    {
            sRenderer = GetComponent<SpriteRenderer>();
            switch(upgrade) 
            {

                case Dash:
                if (playerData.unlockedDash){
                    isCollected = true;
 	                GetComponent<CircleCollider2D>().enabled = false;
	                GetComponent<SpriteRenderer>().enabled = false;                   
                }
                else{
                   sRenderer.sprite = sprites[0]; 
                }          
                break;

                case WallJump:
                if (playerData.unlockedWallJump){
                    isCollected = true;
 	                GetComponent<CircleCollider2D>().enabled = false;
	                GetComponent<SpriteRenderer>().enabled = false;                   
                }
                else{
                   sRenderer.sprite = sprites[5]; 
                }          
                break;

                case Fireball:
                if (playerData.unlockedFireball){
                    isCollected = true;
 	                GetComponent<CircleCollider2D>().enabled = false;
	                GetComponent<SpriteRenderer>().enabled = false;                   
                }
                else{
                   sRenderer.sprite = sprites[1]; 
                }          
                break;

                case ChargeAttack:
                if (playerData.unlockedChargeAttack){
                    isCollected = true;
 	                GetComponent<CircleCollider2D>().enabled = false;
	                GetComponent<SpriteRenderer>().enabled = false;                   
                }
                else{
                   sRenderer.sprite = sprites[4]; 
                }                             
                break;

                case DoubleJump:
                if (playerData.amountOfJumps == 2){
                    isCollected = true;
 	                GetComponent<CircleCollider2D>().enabled = false;
	                GetComponent<SpriteRenderer>().enabled = false;                   
                }
                else{
                   sRenderer.sprite = sprites[3]; 
                }                              
                break;
                case Healing:
                if (playerData.unlockedHealing){
                    isCollected = true;
 	                GetComponent<CircleCollider2D>().enabled = false;
	                GetComponent<SpriteRenderer>().enabled = false;                   
                }
                else{
                   sRenderer.sprite = sprites[2]; 
                }          
                break;
                
                default:
                Debug.Log("No upgrade set you idiot!");
                break;
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {	
            switch(upgrade) 
            {
                case Healing:
                playerData.unlockedHealing = true;
                break;

                case Dash:
                playerData.unlockedDash = true;
                break;

                case WallJump:
                playerData.unlockedWallJump = true;
                break;

                case Fireball:
                playerData.unlockedFireball = true;
                break;

                case ChargeAttack:
                playerData.unlockedChargeAttack = true;
                break;

                case DoubleJump:
                playerData.amountOfJumps = 2;
                break;

                default:
                Debug.Log("No upgrade set you idiot!");
                break;
            }
            isCollected = true;
            text.SetActive(true);
            GetComponent<CircleCollider2D>().enabled = false;
	        GetComponent<SpriteRenderer>().enabled = false;
            

        }    
    }
}
