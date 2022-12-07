using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState {
    
    private int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();

        player.SetVelocityY(playerData.jumpVelocity);

        GameObject jParticle = MonoBehaviour.Instantiate(player.jumpParticleContainer, player.transform);
        jParticle.transform.position = new Vector3(player.transform.position.x, player.GetComponent<BoxCollider2D>().bounds.min.y+.1f, player.transform.position.z);
        jParticle.GetComponent<ParticleSystem>().Play();
        MonoBehaviour.Destroy(jParticle, 1f);

        if (player.CheckIfGrounded())
        {
            player.AudioSource.PlayOneShot(player.jump);
        }
        else
        {
            player.AudioSource.PlayOneShot(player.airJump);
        }

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


        isAbilityDone = true;

        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if(amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
