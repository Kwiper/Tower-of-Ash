using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshenBelcherWalk : EnemyState {

    AshenBelcher belcher;
    GameObject player;

    float timer;
    float maxTimer = 2f;

    public AshenBelcherWalk(AshenBelcher enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.belcher = enemy;
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

        if (belcher.CheckIfTouchingWall())
        {
            belcher.Flip();
        }

        if (!belcher.CheckIfTouchingLedge())
        {
            belcher.Flip();
        }

        if (!belcher.EnemyEntity.InKnockback)
        {
            belcher.SetVelocityX(2 * belcher.FacingDirection);
        }
        else
        {
            belcher.SetVelocityX(belcher.EnemyEntity.Knockback);
        }

        if (belcher.CheckIfPlayerInAggro() && player.transform.position.y >= belcher.transform.position.y && timer <= 0)
        {
            belcher.StateMachine.ChangeState(belcher.BelchState);
        }


    }
}
