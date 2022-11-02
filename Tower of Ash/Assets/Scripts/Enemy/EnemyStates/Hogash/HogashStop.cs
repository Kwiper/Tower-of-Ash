using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogashStop : EnemyState {

    Hogash hog;

    public HogashStop(Hogash enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.hog = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        hog.StateMachine.ChangeState(hog.WalkState);
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
