using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAttackState : PlayerAbilityState {

    private int attackCounter = 0;

    private int yInput;

    private Hitbox hitbox;

    private bool stepForward;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        stepForward = !stepForward;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        hitbox = player.gameObject.GetComponentInChildren<Hitbox>(true);

        player.InputHandler.UseAttackInput();

        stepForward = false;

        yInput = player.InputHandler.NormInputY;
        player.Anim.SetInteger("yInput", yInput); 

        if (yInput == 0 && isGrounded) attackCounter++;
        
        if (attackCounter > 2)
        {
            ResetAttackCounter();
        }
        
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Anim.SetInteger("attackCounter", attackCounter);

        if (stepForward)
        {
            player.SetVelocityX(4 * player.FacingDirection);
        }
        else
        {
            player.SetVelocityX(0);
        }

        if(yInput < 0 && player.CurrentVelocity.y < 0)
        {
            if (hitbox.HitObject)
            {
                player.SetVelocityY(playerData.pogoVelocity);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void ResetAttackCounter() => attackCounter = 0;


}
