using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState {


    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip(xInput);
        
        player.SetVelocityX((playerData.movementVelocity) * xInput);
        
        if(xInput == 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        int rng = Random.Range(0, 4);

        switch (rng)
        {
            default:
                Debug.Log("no sound effect loaded");
                break;
            case 0:
                player.AudioSource.PlayOneShot(player.footstep1);
                player.AudioSource.PlayOneShot(player.cape1,0.3f);
                break;

            case 1:
                player.AudioSource.PlayOneShot(player.footstep2);
                player.AudioSource.PlayOneShot(player.cape2,0.3f);
                break;

            case 2:
                player.AudioSource.PlayOneShot(player.footstep3);
                player.AudioSource.PlayOneShot(player.cape3,0.3f);
                break;

            case 3:
                player.AudioSource.PlayOneShot(player.footstep4);
                player.AudioSource.PlayOneShot(player.cape4,0.3f);
                break;
        }

    }
}
