using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSkipperThrustState : EnemyState {

    HellSkipper skipper;

    public HellSkipperThrustState(HellSkipper enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.skipper = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        skipper.StateMachine.ChangeState(skipper.WalkState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        // Fire projectile
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

        if (!skipper.EnemyEntity.InKnockback)
        {
            skipper.SetVelocityX(0);
        }
        else
        {
            skipper.SetVelocityX(skipper.EnemyEntity.Knockback);
        }

    }
}
