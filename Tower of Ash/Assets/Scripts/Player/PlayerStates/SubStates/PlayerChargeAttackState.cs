using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeAttackState : PlayerAbilityState
{

    private bool launch;

    public PlayerChargeAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        launch = !launch;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        
        base.Enter();       
        player.InputHandler.UseChargeAttackInput();
        launch = false;
        player.BoostAttack();
    }

    public override void Exit()
    {
        base.Exit();
        player.ResetAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isGrounded)
        {
            if (launch)
            {
                player.SetVelocityY(playerData.chargeVelocity);
                player.SetVelocityX(playerData.chargeVelocity * player.InputHandler.NormInputX);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        player.AudioSource.PlayOneShot(player.swordBig);
    }
}
