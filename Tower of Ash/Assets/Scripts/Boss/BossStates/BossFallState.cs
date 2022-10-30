using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFallState : EnemyState {

    Boss boss;

    private float bossTimer;
    private float bossMaxTimer = 5f;

    bool canFall;

    public BossFallState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.boss = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        bossTimer = bossMaxTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        bossTimer -= Time.deltaTime;

        if(bossTimer > 4.5f)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.bulletHellPoint.position, Time.deltaTime * 30);
        }
        else if(bossTimer <= 4.5f)
        {
            canFall = true;
        }

        if(canFall && !boss.CheckIfTouchingGround())
        {
            boss.SetVelocityY(-60);
        }

        if(canFall && boss.CheckIfTouchingGround())
        {
            //Pillars
        }

        if(bossTimer <= 0)
        {
            boss.StateMachine.ChangeState(boss.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
