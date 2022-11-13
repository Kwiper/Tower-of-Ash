using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostEmberFireState : EnemyState {

    LostEmber ember;

    public LostEmberFireState(LostEmber enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.ember = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        ember.StateMachine.ChangeState(ember.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        ember.FireEmber();
    }
}
