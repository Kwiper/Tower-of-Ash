using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAttackState : PlayerAbilityState {

    private int attackCounter = 0;

    private int yInput;


    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(attackCounter);
        player.InputHandler.UseAttackInput();

        yInput = player.InputHandler.NormInputY;
        player.Anim.SetInteger("yInput", yInput);

        if (yInput == 0) attackCounter++;
        
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
        player.Anim.SetInteger("attackCounter", attackCounter);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void ResetAttackCounter() => attackCounter = 0;


}
