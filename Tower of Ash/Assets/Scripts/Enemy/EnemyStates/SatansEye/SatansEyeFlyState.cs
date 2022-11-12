using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatansEyeFlyState : EnemyState {

    SatansEye eye;

    private Vector2 direction;
    GameObject player;

    bool nearPlayer;

    float timer;
    float maxTimer = 4f;

    public SatansEyeFlyState(SatansEye enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.eye = enemy;
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
        eye.CheckIfShouldFlip();

        direction = new Vector2(player.transform.position.x - eye.transform.position.x, player.transform.position.y - eye.transform.position.y).normalized;

        if (!eye.EnemyEntity.InKnockback)
        {
            if (!eye.CheckIfPlayerInAggroRange())
            {
                eye.SetVelocityX(0);
                eye.SetVelocityY(0);
            }
            else
            {
                eye.SetVelocity(2, direction);
            }
        }
        else
        {
            eye.SetVelocityX(eye.EnemyEntity.Knockback);
        }

        if (eye.CheckIfNearPlayer())
        {
            nearPlayer = true;
        }

        if (nearPlayer)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            eye.StateMachine.ChangeState(eye.FireState);
        }

    }
}
