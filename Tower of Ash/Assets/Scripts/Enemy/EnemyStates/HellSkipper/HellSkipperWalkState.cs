using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSkipperWalkState : EnemyState {

    HellSkipper skipper;
    bool move;

    float timer;
    float maxTimer = 3f;

    GameObject player;

    public HellSkipperWalkState(HellSkipper enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.skipper = enemy;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        move = !move;
    }

    public override void Enter()
    {
        base.Enter();
        move = false;
        timer = maxTimer;

        player = FindObjectOfType<Player>().gameObject;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer -= Time.deltaTime;

        if (skipper.CheckIfTouchingWall())
        {
            skipper.Flip();
        }

        if (!skipper.CheckIfTouchingLedge())
        {
            skipper.Flip();
        }

        if (!skipper.EnemyEntity.InKnockback)
        {
            if (move)
            {
                skipper.SetVelocityX(3 * skipper.FacingDirection);
            }
            else
            {
                skipper.SetVelocityX(0);
            }
        }
        else
        {
            skipper.SetVelocityX(skipper.EnemyEntity.Knockback);
        }

        if(timer <= 0)
        {
            if(skipper.CheckIfPlayerInAggro() && player.transform.position.y < skipper.transform.position.y)
            {
                skipper.StateMachine.ChangeState(skipper.JumpState);
            }
            else if(skipper.CheckIfPlayerInAggro() && player.transform.position.y >= skipper.transform.position.y)
            {
                int random = Random.Range(0, 2);

                switch (random)
                {
                    case 0:
                        skipper.StateMachine.ChangeState(skipper.ThrustState);
                        break;
                    case 1:
                        skipper.StateMachine.ChangeState(skipper.JumpState);
                        break;
                }

            }
        }
    }
}
