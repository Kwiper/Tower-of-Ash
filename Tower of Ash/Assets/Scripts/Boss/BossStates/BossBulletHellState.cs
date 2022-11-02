using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletHellState : EnemyState
{

    private Boss boss;

    private float bulletHellTimer;
    private float bulletHellMaxTimer = 8f;

    public BossBulletHellState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
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
        bulletHellTimer = bulletHellMaxTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        boss.SetVelocityX(0);
        boss.SetVelocityY(0);
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.bulletHellPoint.position, Time.deltaTime * 30);

        bulletHellTimer -= Time.deltaTime;
        if (bulletHellTimer <= 0)
        {
            boss.StateMachine.ChangeState(boss.FallState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
