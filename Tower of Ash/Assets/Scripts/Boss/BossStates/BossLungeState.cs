using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLungeState : EnemyState {

    private Boss boss;

    private bool isLunging = false;

    public BossLungeState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.boss = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        boss.StateMachine.ChangeState(boss.IdleState);
        
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isLunging = !isLunging;

    }

    public override void Enter()
    {
        base.Enter();
        isLunging = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isLunging)
        {
            if (!boss.CheckIfBoundaryDetected())
            {
                boss.SetVelocityX(100 * boss.FacingDirection);
            }
            else
            {
                boss.SetVelocityX(0.5f);
            }
        }
        else
        {
            boss.SetVelocityX(0);
        }

    }
}
