using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialMenu : MonoBehaviour
{
    GameObject playerObject;

    Player player;

    PlayerInput playerInput;

    [SerializeField]
    GameObject[] keyboardTexts;
    [SerializeField]
    GameObject[] controllerTexts;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = FindObjectOfType<Player>().gameObject;
        player = playerObject.GetComponent<Player>();
        playerInput = playerObject.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.InputHandler.InteractInput)
        {
            player.InputHandler.UseInteractInput();
            PauseMenu.GameIsPaused = false;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }

        if (playerInput.currentControlScheme == "Keyboard")
        {
            foreach (GameObject i in keyboardTexts)
            {
                i.gameObject.SetActive(true);
            }
            foreach (GameObject i in controllerTexts)
            {
                i.gameObject.SetActive(false);
            }
        }
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            foreach (GameObject i in keyboardTexts)
            {
                i.gameObject.SetActive(false);
            }
            foreach (GameObject i in controllerTexts)
            {
                i.gameObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
    }
}
