using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGolemChargeState : EnemyState {

    BlackGolem golem;

    public BlackGolemChargeState(BlackGolem enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.golem = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        golem.StateMachine.ChangeState(golem.RunState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        golem.SetVelocityX(0);
    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        golem.AudioSource.PlayOneShot(golem.slam);
    }
}
