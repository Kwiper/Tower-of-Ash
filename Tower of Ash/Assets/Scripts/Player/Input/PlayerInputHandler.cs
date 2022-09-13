using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 movementInput;


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Move input");
        movementInput = context.ReadValue<Vector2>();
        Debug.Log(movementInput);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Jump button pushed down now");
        }

        if (context.performed)
        {
            Debug.Log("Jump button being held down now");
        }

        if (context.canceled)
        {
            Debug.Log("Jump button has been released");
        }

        //Debug.Log("Jump input");
    }

}
