using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireballState : PlayerAbilityState {

    public bool CanFireball { get; private set; }

    private float lastFireballTime;

    public PlayerFireballState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanFireball = false;
        player.InputHandler.UseFireballInput();
       
        startTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.SetVelocityY(0);
            player.SetVelocityX(5 * -player.FacingDirection);

            if (Time.time >= startTime + playerData.fireballTime)
            {
                isAbilityDone = true;
                lastFireballTime = Time.time;
            }
        }

    }

    public bool CheckIfCanFireball()
    {
        return CanFireball && Time.time >= lastFireballTime + playerData.fireballCooldown;
    }

    public void ResetCanFireball() => CanFireball = true;

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        Debug.Log("Animation triggered");
        player.CastFireball();
    }
}
