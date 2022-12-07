using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGolemSlamState : EnemyState {

    BlackGolem golem;

    float distance;
    float initialDistance = 3f;

    public BlackGolemSlamState(BlackGolem enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.golem = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        golem.StateMachine.ChangeState(golem.WalkState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        golem.CastFirePillar(distance);
        distance += 3f;

    }

    public override void Enter()
    {
        base.Enter();
        distance = initialDistance;
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
