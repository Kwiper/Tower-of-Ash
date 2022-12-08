using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Bonfire : MonoBehaviour
{

    Player player;

    [SerializeField]
    Transform rangePoint;
    [SerializeField]
    float range;
    [SerializeField]
    LayerMask playerLayer;

    AudioSource audioSource;
    [SerializeField]
    AudioClip bonfireHeal;

    [SerializeField]
    TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (CheckIfPlayerInRange())
        {
            text.gameObject.SetActive(true);

            if (player.InputHandler.InteractInput && player.CheckIfGrounded())
            {
                player.InputHandler.UseInteractInput();
                player.ResetHealCharges();
                player.ResetHealth();
                audioSource.PlayOneShot(bonfireHeal);
            }
        }
        else
        {
            text.gameObject.SetActive(false);
        }

        if(player.gameObject.GetComponent<PlayerInput>().currentControlScheme == "Keyboard")
        {
            text.text = "Press E to interact";
        }
        else if(player.gameObject.GetComponent<PlayerInput>().currentControlScheme == "Gamepad")
        {
            text.text = "Press B to interact";
        }

    }

    private bool CheckIfPlayerInRange()
    {
        return Physics2D.OverlapCircle(rangePoint.position, range, playerLayer);
    }

}
