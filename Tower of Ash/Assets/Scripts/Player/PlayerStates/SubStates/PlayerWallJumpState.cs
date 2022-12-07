using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState {

    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();

        player.JumpState.ResetAmountOfJumpsLeft();

        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.CheckIfShouldFlip(wallJumpDirection);

        GameObject jParticle = MonoBehaviour.Instantiate(player.jumpParticleContainer, player.transform);
        jParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y-.5f, player.transform.position.z);
        jParticle.GetComponent<ParticleSystem>().Play();
        MonoBehaviour.Destroy(jParticle, 1f);

        player.AudioSource.PlayOneShot(player.jump);

        int rng = Random.Range(0, 4);
        switch (rng)
        {
            default:

                break;
            case 0:
                player.AudioSource.PlayOneShot(player.cape1);
                break;
            case 1:
                player.AudioSource.PlayOneShot(player.cape2);
                break;
            case 2:
                player.AudioSource.PlayOneShot(player.cape3);
                break;
            case 3:
                player.AudioSource.PlayOneShot(player.cape4);
                break;
        }

        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

        if(Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDetection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -player.FacingDirection;
        }
        else
        {
            wallJumpDirection = player.FacingDirection;
        }
    }
}
