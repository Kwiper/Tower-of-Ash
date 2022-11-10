using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSkipperDownState : EnemyState {

    public Vector2 direction;

    HellSkipper skipper;

    bool isThrusting;

    public HellSkipperDownState(HellSkipper enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.skipper = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        skipper.StateMachine.ChangeState(skipper.WalkState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isThrusting = false;
    }

    public override void Enter()
    {
        base.Enter();
        isThrusting = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isThrusting)
        {
            skipper.SetVelocity(30, direction);
        }
        else
        {
            skipper.SetVelocityX(skipper.CurrentVelocity.x * 0.75f);
        }

    }
}
