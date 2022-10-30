using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletHellCharge : EnemyState
{
    private Boss boss;

    private bool canMove;

    private float chargeTimer;
    private float chargeMaxTimer = 3f;

    public BossBulletHellCharge(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.boss = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        chargeTimer = chargeMaxTimer;
        canMove = false;
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

        chargeTimer -= Time.deltaTime;

        if(chargeTimer <= 2)
        {
            canMove = true;
        }

        if(chargeTimer <= 0)
        {
            boss.StateMachine.ChangeState(boss.BulletHellState);
        }

        if (canMove)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.bulletHellPoint.position, Time.deltaTime * 30);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
