using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalkState : EnemyState {

    private Boss boss;
    private float walkTime;
    private float maxWalkTime = 1f;

    public BossWalkState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        boss = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered walk state");

        walkTime = maxWalkTime;
    }

    public override void Exit()
    {
        base.Exit();
        boss.SetVelocityX(0);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        walkTime -= Time.deltaTime;

        if(walkTime > 0)
        {
            boss.SetVelocityX(5 * boss.FacingDirection);
        }

        if (walkTime <= 0)
        {
            if (boss.CheckIfPlayerInAggroRange())
            {
                boss.StateMachine.ChangeState(boss.AttackState);
            }
            else if (!boss.CheckIfPlayerInAggroRange() && boss.CheckIfPlayerInProjectileRadius())
            {
                boss.StateMachine.ChangeState(boss.IdleState);
            } 
            else if (!boss.CheckIfPlayerInProjectileRadius())
            {
                boss.StateMachine.ChangeState(boss.LungeState);
            }
        }
    }
}
