using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGroundedState : PlayerState {

    protected int xInput;

    private bool JumpInput;
    private bool isGrounded;
    private bool dashInput;
    private bool fireballInput;
    private bool chargeAttackInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetBool("isGrounded", true);

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;
        fireballInput = player.InputHandler.FireballInput;
        chargeAttackInput = player.InputHandler.ChargeAttackInput;

        if (chargeAttackInput)
        {
            stateMachine.ChangeState(player.ChargeAttackState);
        }
        else if (fireballInput && player.FireballState.CheckIfCanFireball())
        {
            stateMachine.ChangeState(player.FireballState);
        }
        else if (player.InputHandler.AttackInput)
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if (JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        } 
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
