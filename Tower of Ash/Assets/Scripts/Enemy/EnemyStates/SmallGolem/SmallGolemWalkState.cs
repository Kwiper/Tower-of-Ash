using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGolemWalkState : EnemyState {

    SmallGolem golem;

    GameObject player;

    private float golemTimer;
    private float golemMaxTimer = 3f;

    public SmallGolemWalkState(SmallGolem enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.golem = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = FindObjectOfType<Player>().gameObject;

        golemTimer = golemMaxTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        golemTimer -= Time.deltaTime;

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
            golem.SetVelocityX(1.5f * golem.FacingDirection);
        }
        else
        {
            golem.SetVelocityX(golem.EnemyEntity.Knockback);
        }

        if(golem.CheckIfPlayerInAggroRange() && player.transform.position.y >= golem.transform.position.y)
        {
            if(golemTimer <= 0)
                golem.StateMachine.ChangeState(golem.ThrowState);
        }

    }
}
