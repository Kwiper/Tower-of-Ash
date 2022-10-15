using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerState
{

    private int xInput;
    private bool isGrounded;

    public PlayerHitState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        player.isHit = false;
        player.SetVelocityX(0);
        player.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isGrounded)
        {
            player.SetVelocityY(0);
        }
        else
        {
            player.SetVelocityY(7);
        }
    }

}
