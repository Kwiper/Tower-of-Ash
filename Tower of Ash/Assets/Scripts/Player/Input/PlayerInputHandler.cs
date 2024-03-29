using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawLookInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public int NormLookInputX { get; private set; }
    public int NormLookInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool FireballInput { get; private set; }
    public bool ChargeAttackInput { get; private set; }
    public bool HealInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool MapInput { get; private set; }
    public bool PauseInput { get; private set; }
    public bool LoadInput { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;
    [SerializeField]
    private int lookAdjustmentSize = 2;
    private float jumpInputStartTime;
    private float dashInputStartTime;
    private float fireballInputStartTime;

    public bool chargeHeld = false;

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        CheckFireballInputHoldTime();
    }

    public void OnLoadInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            LoadInput = true;
        }
        if (context.canceled)
        {
            LoadInput = false;
        }
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PauseInput = true;
        }
        if (context.canceled)
        {
            PauseInput = false;
        }
    }

    public void OnMapInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            MapInput = true;
        }
        if (context.canceled)
        {
            MapInput = false;
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractInput = true;
        }
        if (context.canceled)
        {
            InteractInput = false;
        }
    }

    public void OnHealInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            HealInput = true;
        }
        if (context.canceled)
        {
            HealInput = false;
        }
    }

    public void OnChargeAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Charge Held");
            chargeHeld = true;
        }
        if(context.canceled)
        {
            if (chargeHeld)
            {
                chargeHeld = false;
                ChargeAttackInput = true;
                Debug.Log("Charge attack!");
            }
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput = true;
        }
    }
    public void OnFireballInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            FireballInput = true;
            fireballInputStartTime = Time.time;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        RawLookInput = context.ReadValue<Vector2>();

        NormLookInputX = Mathf.RoundToInt(RawLookInput.x*lookAdjustmentSize);
        NormLookInputY = Mathf.RoundToInt(RawLookInput.y*lookAdjustmentSize);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            //Debug.Log("Jump input cancelled");
            JumpInputStop = true;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            dashInputStartTime = Time.time;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void UseDashInput() => DashInput = false;

    public void UseAttackInput() => AttackInput = false;
    public void UseFireballInput() => FireballInput = false;
    public void UseChargeAttackInput() => ChargeAttackInput = false;

    public void UseHealInput() => HealInput = false;
    public void UseInteractInput() => InteractInput = false;
    public void UseMapInput() => MapInput = false;
    public void UsePauseInput() => PauseInput = false;
    public void UseLoadInput() => LoadInput = false;

    private void CheckJumpInputHoldTime() // Jump buffer
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckDashInputHoldTime() // Dash buffer
    {
        if(Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    } 
    private void CheckFireballInputHoldTime()
    {
        if(Time.time >= fireballInputStartTime + inputHoldTime)
        {
            FireballInput = false;
        }
    }


}
