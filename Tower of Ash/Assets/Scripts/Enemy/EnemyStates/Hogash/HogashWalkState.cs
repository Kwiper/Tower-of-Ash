using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogashWalkState : EnemyState {

    Hogash hog;
    GameObject player;

    public HogashWalkState(Hogash enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.hog = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = FindObjectOfType<Player>().gameObject;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (hog.CheckIfTouchingWall())
        {
            hog.Flip();
        }

        if (!hog.CheckIfTouchingLedge())
        {
            hog.Flip();
        }

        if (!hog.EnemyEntity.InKnockback)
        {
            hog.SetVelocityX(2 * hog.FacingDirection);
        }
        else
        {
            hog.SetVelocityX(hog.EnemyEntity.Knockback);
        }

        if (hog.CheckIfPlayerInAggro())
        {
            hog.StateMachine.ChangeState(hog.ChargeState);
        }

    }
}
