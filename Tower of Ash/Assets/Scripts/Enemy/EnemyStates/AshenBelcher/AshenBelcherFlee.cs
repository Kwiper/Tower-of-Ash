using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshenBelcherFlee : EnemyState {

    AshenBelcher belcher;

    float timer;
    float maxTimer = 2f;

    public AshenBelcherFlee(AshenBelcher enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.belcher = enemy;
    }

    public override void Enter()
    {
        base.Enter();
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


        if (!belcher.EnemyEntity.InKnockback)
        {
            belcher.SetVelocityX(6 * belcher.FacingDirection);
        }
        else
        {
            belcher.SetVelocityX(belcher.EnemyEntity.Knockback);
        }

        if (belcher.CheckIfTouchingWall())
        {
            belcher.StateMachine.ChangeState(belcher.WalkState);
        }

        if (belcher.CheckIfPlayerInAggro() && timer <= 0)
        {
            belcher.StateMachine.ChangeState(belcher.BelchState);
        }

    }
}
