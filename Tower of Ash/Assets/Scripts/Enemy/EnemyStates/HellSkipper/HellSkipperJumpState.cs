using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSkipperJumpState : EnemyState {

    HellSkipper skipper;

    GameObject player;

    bool isJumping;

    public HellSkipperJumpState(HellSkipper enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.skipper = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        skipper.StateMachine.ChangeState(skipper.DownState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isJumping = !isJumping;
    }

    public override void Enter()
    {
        base.Enter();
        isJumping = false;
        player = FindObjectOfType<Player>().gameObject;
    }

    public override void Exit()
    {
        base.Exit();
        skipper.DownState.direction = new Vector2(player.transform.position.x - skipper.transform.position.x, player.transform.position.y - skipper.transform.position.y).normalized;
        if(skipper.DownState.direction.y > 0)
        {
            skipper.DownState.direction.y *= -1;
        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        skipper.CheckIfShouldFlip();

        skipper.SetVelocityX(0);

        if (!isJumping)
        {
            skipper.SetVelocityY(0);
        }
        else
        {
            skipper.SetVelocityY(8);
        }

    }
}
