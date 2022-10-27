using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : EnemyState
{

    private float maxTimer = 2f;
    private float timer;
    private Boss boss;
    private string animBoolName;

    public BossIdleState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.boss = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        timer = maxTimer;
    }

    public override void Exit()
    {
        base.Exit();
        boss.SetVelocityX(0);
        boss.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        boss.CheckIfShouldFlip();
        timer -= Time.deltaTime;


        if(timer <= 0)
        {
            if (!boss.CheckIfPlayerInAggroRange() && boss.CheckIfPlayerInProjectileRadius())
            {
                boss.StateMachine.ChangeState(boss.WalkState);
            }
            else if(boss.CheckIfPlayerInAggroRange())
            {
                boss.StateMachine.ChangeState(boss.AttackState);
            }
            else if (!boss.CheckIfPlayerInProjectileRadius())
            {
                boss.StateMachine.ChangeState(boss.LungeState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
