using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGolemWalkState : EnemyState {

    BlackGolem golem;

    GameObject player;

    float timer;
    float maxTimer = 5f;

    public BlackGolemWalkState(BlackGolem enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.golem = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = FindObjectOfType<Player>().gameObject;
        timer = maxTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        timer -= Time.deltaTime;

        if (golem.CheckIfTouchingWall())
        {
            golem.Flip();
        }

        if (!golem.CheckIfTouchingLedge())
        {
            golem.Flip();
        }

        if (!golem.EnemyEntity.InKnockback)
        {
            golem.SetVelocityX(1f * golem.FacingDirection);
        }
        else
        {
            golem.SetVelocityX(golem.EnemyEntity.Knockback);
        }

        if (timer <= 0)
        {
            if (golem.CheckIfPlayerInChargeRadius() && player.transform.position.y >= golem.transform.position.y - 0.5)
            {
                golem.StateMachine.ChangeState(golem.ChargeState);
            }

            if (golem.CheckIfPlayerInSlamRadius() && player.transform.position.y >= golem.transform.position.y - 0.5)
            {
                golem.StateMachine.ChangeState(golem.SlamState);
            }
        }

    }
}
