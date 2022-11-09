using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurntVineSpitState : EnemyState
{
    BurntVine vine;

    public BurntVineSpitState(BurntVine enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.vine = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        vine.StateMachine.ChangeState(vine.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        vine.SpitAsh();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
