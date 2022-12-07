using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TextVariation : MonoBehaviour
{
    TextMeshProUGUI text;

    [SerializeField]
    string keyboardText;

    [SerializeField]
    string controllerText;

    PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        playerInput = FindObjectOfType<Player>().GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.currentControlScheme == "Keyboard")
        {
            text.text = keyboardText;
        }
        else if(playerInput.currentControlScheme == "Gamepad")
        {
            text.text = controllerText;
        }
    }
}
