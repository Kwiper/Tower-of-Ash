using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WailingPhantomFloatState : EnemyState {

    WailingPhantom phantom;
    private Vector2 direction;
    GameObject player;

    bool nearPlayer;

    float timer;
    float maxTimer = 1.5f;

    public WailingPhantomFloatState(WailingPhantom enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.phantom = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = FindObjectOfType<Player>().gameObject;
        nearPlayer = false;

        timer = maxTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        phantom.CheckIfShouldFlip();

        direction = new Vector2(player.transform.position.x - phantom.transform.position.x, player.transform.position.y - phantom.transform.position.y).normalized;

        if (!phantom.EnemyEntity.InKnockback)
        {
            if (!phantom.CheckIfPlayerInAggroRange()) 
            {
                phantom.SetVelocityX(0);
                phantom.SetVelocityY(0);
            }
            else
            {
                phantom.SetVelocity(2, direction);
            }
        }
        else
        {
            phantom.SetVelocityX(phantom.EnemyEntity.Knockback);
        }

        if (phantom.CheckIfNearPlayer())
        {
            nearPlayer = true;
        }

        if (nearPlayer)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0)
        {
            phantom.StateMachine.ChangeState(phantom.ChargeState);
        }
    }
}
