using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurntVineSwipeState : EnemyState
{
    BurntVine vine;

    public BurntVineSwipeState(BurntVine enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.vine = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        vine.StateMachine.ChangeState(vine.IdleState);
    }
}
