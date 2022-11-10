using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogashRunState : EnemyState {

    Hogash hog;

    public HogashRunState(Hogash enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.hog = enemy;
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

        if (!hog.EnemyEntity.InKnockback)
        {
            hog.SetVelocityX(8 * hog.FacingDirection);
        }
        else
        {
            hog.SetVelocityX(hog.EnemyEntity.Knockback);
        }

        if (hog.CheckIfTouchingWall())
        {
            hog.StateMachine.ChangeState(hog.StopState);
        }

    }
}
