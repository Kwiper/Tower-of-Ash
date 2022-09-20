using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool FireballInput { get; private set; }
    public bool ChargeAttackInput { get; private set; }
    public bool CanChargeAttack { get; private set; }


    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;
    private float fireballInputStartTime;

    [SerializeField]
    private float holdForChargeStart = 0.8f;

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        CheckFireballInputHoldTime();
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput = true;
            ResetHoldForChargeStart();
        }
        if (context.performed)
        {
            if(holdForChargeStart > 0) holdForChargeStart--;
            if(holdForChargeStart <= 0)
            {
                CanChargeAttack = true;
                Debug.Log("Attack charged");
            }
        }
        if(context.canceled && CanChargeAttack)
        {
            ChargeAttackInput = true;
            Debug.Log("Charge released!");
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

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
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

    public void ResetHoldForChargeStart() => holdForChargeStart = 0.8f;

}
