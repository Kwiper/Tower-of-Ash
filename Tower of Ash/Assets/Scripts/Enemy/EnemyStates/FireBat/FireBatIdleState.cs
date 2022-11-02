using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBatIdleState : EnemyState
{
    FireBat bat;

    public FireBatIdleState(FireBat enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.bat = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        bat.CheckIfShouldFlip();

        if (!bat.EnemyEntity.InKnockback)
        {
            bat.SetVelocityX(0);
            bat.SetVelocityY(0);
        }
        else
        {
            bat.SetVelocityX(bat.EnemyEntity.Knockback);
        }

        if (bat.CheckIfPlayerInAggroRange())
        {
            bat.StateMachine.ChangeState(bat.AggroState);
        }

    }
}
