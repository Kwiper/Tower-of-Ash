using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : EnemyState {

    private Boss boss;
    public BossAttackState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.boss = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        boss.StateMachine.ChangeState(boss.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        boss.AudioSource.PlayOneShot(boss.slam);
    }

    public override void Enter()
    {
        base.Enter();
        boss.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        boss.AudioSource.PlayOneShot(boss.swing);
    }
}
