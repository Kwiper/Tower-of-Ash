using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowRobesFloatState : EnemyState {

    HollowRobes robes;
    private Vector2 direction;
    GameObject player;

    bool isFleeing;

    float fleeTimer;
    float maxFleeTimer = 0.5f;

    float attackTimer;
    float maxAttackTimer = 3f;

    bool canAttack;

    public HollowRobesFloatState(HollowRobes enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.robes = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = FindObjectOfType<Player>().gameObject;

        fleeTimer = maxFleeTimer;
        attackTimer = maxAttackTimer;
        canAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        robes.CheckIfShouldFlip();

        direction = new Vector2(player.transform.position.x - robes.transform.position.x, player.transform.position.y - robes.transform.position.y).normalized;

        if (!robes.EnemyEntity.InKnockback)
        {
            if (!isFleeing)
            {
                if (!robes.CheckIfPlayerInAggro())
                {
                    robes.SetVelocityX(0);
                    robes.SetVelocityY(0);
                }
                else
                {
                    robes.SetVelocity(5, direction);
                }
            }
            else
            {
                robes.SetVelocity(8, -direction);
            }
        }
        else
        {
            robes.SetVelocityX(robes.EnemyEntity.Knockback);
        }

        if (robes.CheckIfInFleeRadius() && !isFleeing)
        {
            isFleeing = true;
            fleeTimer = maxFleeTimer;
        }

        if (robes.CheckIfInPlayerRadius())
        {
            canAttack = true;
        }

        if (canAttack)
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                robes.StateMachine.ChangeState(robes.FireState);
            }
        }

        if (isFleeing)
        {
            fleeTimer -= Time.deltaTime;

            if(fleeTimer <= 0)
            {
                isFleeing = false;
                robes.StateMachine.ChangeState(robes.FireState);
            }
        }

    }
}
