using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatansEyeFireState : EnemyState {

    SatansEye eye;

    public SatansEyeFireState(SatansEye enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.eye = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        eye.StateMachine.ChangeState(eye.FlyState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        eye.Fire();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        eye.CheckIfShouldFlip();

        if (!eye.EnemyEntity.InKnockback)
        {
            eye.SetVelocityX(0);
            eye.SetVelocityY(0);
        }
        else
        {
            eye.SetVelocityX(eye.EnemyEntity.Knockback);
        }
    }
}
