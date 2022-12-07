using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGolemRunState : EnemyState
{
    BlackGolem golem;

    public BlackGolemRunState(BlackGolem enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.golem = enemy;
    }

    public override void Exit()
    {
        base.Exit();

        golem.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        golem.SetVelocityX(5 * golem.FacingDirection);

        if (!golem.CheckIfPlayerInAggroRadius() || golem.CheckIfTouchingWall() || !golem.CheckIfTouchingLedge())
        {
            golem.StateMachine.ChangeState(golem.WalkState);
        }


    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        golem.AudioSource.PlayOneShot(golem.step);
    }
}
