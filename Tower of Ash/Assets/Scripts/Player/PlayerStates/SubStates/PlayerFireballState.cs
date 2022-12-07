using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireballState : PlayerAbilityState {

    public bool CanFireball { get; private set; }

    private float lastFireballTime;

    private bool recoil = false;

    public PlayerFireballState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanFireball = false;
        player.InputHandler.UseFireballInput();
        recoil = false;
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


            if (recoil)
                player.SetVelocityX(5 * -player.FacingDirection);
            else
                player.SetVelocityX(0);

            /*
            if (Time.time >= startTime + playerData.fireballTime)
            {
                isAbilityDone = true;
                lastFireballTime = Time.time;
            }
            */
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
        recoil = true;
        player.CastFireball();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
        lastFireballTime = Time.time;
    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        player.AudioSource.PlayOneShot(player.fireball);
    }
}
