using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerDeathState : PlayerState {
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.ResetDeathState();
        //SaveSystem.SavePlayer(playerData,combatData,upgradeData);
        stateMachine.ChangeState(player.IdleState);
    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        player.AudioSource.PlayOneShot(player.death);
    }
}
