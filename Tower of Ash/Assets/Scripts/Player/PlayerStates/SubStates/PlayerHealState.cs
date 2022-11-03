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
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityX(0);
    }
}
