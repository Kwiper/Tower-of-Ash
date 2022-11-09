using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WailingPhantomAttackState : EnemyState {

    WailingPhantom phantom;
    public Vector2 direction;

    bool attackDone;

    public WailingPhantomAttackState(WailingPhantom enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.phantom = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        attackDone = false;
        phantom.SetVelocity(30, direction);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        attackDone = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        phantom.StateMachine.ChangeState(phantom.FloatState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (attackDone)
        {
            if (!phantom.EnemyEntity.InKnockback)
            {
                phantom.SetVelocityX(0);
                phantom.SetVelocityY(0);
            }
            else
            {
                phantom.SetVelocityX(phantom.EnemyEntity.Knockback);
            }
        }
    }
}
