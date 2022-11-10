using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WailingPhantomChargeState : EnemyState {

    WailingPhantom phantom;

    private Vector2 direction;
    GameObject player;

    bool charged;

    public WailingPhantomChargeState(WailingPhantom enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.phantom = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        phantom.StateMachine.ChangeState(phantom.AttackState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        charged = true;
        phantom.AttackState.direction = new Vector2(player.transform.position.x - phantom.transform.position.x, player.transform.position.y - phantom.transform.position.y).normalized;
    }

    public override void Enter()
    {
        base.Enter();

        player = FindObjectOfType<Player>().gameObject;
        charged = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!charged)
        {
            phantom.CheckIfShouldFlip();
        }

        direction = new Vector2(player.transform.position.x - phantom.transform.position.x, player.transform.position.y - phantom.transform.position.y).normalized;

        if (!phantom.EnemyEntity.InKnockback)
        {
            if (!charged)
            {
                phantom.SetVelocity(1, direction);
            }
            else
            {
                phantom.SetVelocityX(0);
                phantom.SetVelocityY(0);
            }
        }
        else
        {
            phantom.SetVelocityX(phantom.EnemyEntity.Knockback);
        }

    }
}
