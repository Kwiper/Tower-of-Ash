using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player.InputHandler.InteractInput)
            {
                player.InputHandler.UseInteractInput();
                player.ResetHealCharges();
                player.ResetHealth();
            }
        }
    }
}
