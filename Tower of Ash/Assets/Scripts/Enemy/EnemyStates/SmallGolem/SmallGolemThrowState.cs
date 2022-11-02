using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmallGolemThrowState : EnemyState {

    SmallGolem golem;

    public SmallGolemThrowState(SmallGolem enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.golem = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        golem.StateMachine.ChangeState(golem.WalkState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        golem.ThrowRock();
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

        golem.SetVelocityX(0);
    }
}
