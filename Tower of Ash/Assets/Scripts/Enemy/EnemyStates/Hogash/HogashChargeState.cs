using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogashChargeState : EnemyState {

    Hogash hog;
    public HogashChargeState(Hogash enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.hog = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        hog.StateMachine.ChangeState(hog.RunState);
    }

    public override void Enter()
    {
        base.Enter();
        hog.SetVelocityX(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!hog.EnemyEntity.InKnockback)
        {
            hog.SetVelocityX(0);
        }
        else
        {
            hog.SetVelocityX(hog.EnemyEntity.Knockback);
        }
    }
}
