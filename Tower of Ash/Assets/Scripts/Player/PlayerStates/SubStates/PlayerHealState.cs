using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerAbilityState {
    public PlayerHealState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        player.Heal();
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseHealInput();
    }

    public override void Exit()
    {
        player.healthCanCountdown = true;
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityX(0);
    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        player.AudioSource.PlayOneShot(player.heal);
    }
}
