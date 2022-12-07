using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState {
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.AudioSource.PlayOneShot(player.touchWall);
        player.AudioSource.clip = player.wallSlide;
        player.AudioSource.loop = true;
        player.AudioSource.Play();

    }

    public override void Exit()
    {
        base.Exit();
        player.AudioSource.clip = null;
        player.AudioSource.Stop();
        player.AudioSource.loop = false;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            player.SetVelocityY(-playerData.wallSlideVelocity);
        }
    }
}
