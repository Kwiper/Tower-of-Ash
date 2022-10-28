using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpAttackState : EnemyState {

    private Boss boss;
    private bool hasJumped;

    private Vector2 direction;
    private GameObject player;

    public BossJumpAttackState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.boss = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        boss.StateMachine.ChangeState(boss.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        hasJumped = true;
        boss.SetVelocity(30, direction);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        hasJumped = false;

        player = FindObjectOfType<Player>().gameObject;

        direction = new Vector2(player.transform.position.x - boss.transform.position.x, player.transform.position.y - boss.transform.position.y).normalized;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (hasJumped && boss.CheckIfTouchingGround() && boss.CurrentVelocity.y <= 0)
        {
            boss.SetVelocityX(boss.CurrentVelocity.x * 0.75f);

            // Trigger shockwave
        }
    }
}
