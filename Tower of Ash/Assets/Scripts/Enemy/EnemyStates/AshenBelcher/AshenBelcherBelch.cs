using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshenBelcherBelch : EnemyState {

    AshenBelcher belcher;
    public AshenBelcherBelch(AshenBelcher enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.belcher = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        belcher.StateMachine.ChangeState(belcher.FleeState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        belcher.Belch();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        belcher.Flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        belcher.SetVelocityX(0);

    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        belcher.AudioSource.PlayOneShot(belcher.belch);
    }
}
