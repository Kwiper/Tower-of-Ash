using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAttackState : PlayerAbilityState {

    private int attackCounter = 0;

    private int yInput;
    private int xInput;

    private Hitbox hitbox;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        hitbox = player.gameObject.GetComponentInChildren<Hitbox>(true);

        player.InputHandler.UseAttackInput();

        yInput = player.InputHandler.NormInputY;
        player.Anim.SetInteger("yInput", yInput); 

        if (yInput == 0 && isGrounded) attackCounter++;
        
        if (attackCounter > 2)
        {
            ResetAttackCounter();
        }
        
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;

        player.Anim.SetInteger("attackCounter", attackCounter);

        player.SetVelocityX(playerData.movementVelocity * xInput);

        if (yInput < 0 && player.CurrentVelocity.y < 0)
        {
            if (hitbox.HitObject)
            {
                player.SetVelocityY(playerData.pogoVelocity);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void ResetAttackCounter() => attackCounter = 0;

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();

        int rng = Random.Range(0, 3);

        switch (rng)
        {
            case 0:
                player.AudioSource.PlayOneShot(player.sword1);
                break;

            case 1:
                player.AudioSource.PlayOneShot(player.sword2);
                break;

            case 2:
                player.AudioSource.PlayOneShot(player.sword3);
                break;

            default:
                Debug.Log("No sound effect loaded");
                break;
        }

    }
}
