using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletHellCharge : EnemyState
{
    private Boss boss;

    private bool canMove;

    private float chargeTimer;
    private float chargeMaxTimer = 1.5f;

    Vector2 ballPosition;
    bool setBallPosition;

    public BossBulletHellCharge(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.boss = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        boss.StateMachine.ChangeState(boss.BulletHellState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        setBallPosition = true;
    }

    public override void Enter()
    {
        base.Enter();

        chargeTimer = chargeMaxTimer;
        canMove = false;
        setBallPosition = false;

        ballPosition = new Vector2(boss.transform.position.x + (0.8f * -boss.FacingDirection),boss.transform.position.y + 4.2f);

        boss.canBulletHell = false;
    }

    public override void Exit()
    {
        base.Exit();

    }

    // Increase Y by 4.2
    // Increase X by 0.8 * -FacingDirection 

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        boss.SetVelocityX(0);
        boss.SetVelocityY(0);

        chargeTimer -= Time.deltaTime;

        if(chargeTimer <= 0)
        {
            canMove = true;
        }

        if(setBallPosition && !canMove)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, ballPosition,100);
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
