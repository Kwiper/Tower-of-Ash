using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPedestal : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private int upgrade;

    const int Dash = 1;
    const int WallJump = 2;
    const int Fireball = 3;
    const int ChargeAttack = 4;
    const int DoubleJump = 5;      

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {	
            switch(upgrade) 
            {
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
                playerData.unlockedDoubleJump = true;
                break;

                default:
                Debug.Log("No upgrade set you idiot!");
                break;
            }

	        GetComponent<BoxCollider2D>().enabled = false;
	        GetComponent<SpriteRenderer>().enabled = false;
        }    
    }
}
