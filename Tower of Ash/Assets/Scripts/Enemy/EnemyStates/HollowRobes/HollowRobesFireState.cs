using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowRobesFireState : EnemyState {

    HollowRobes robes;

    bool hasFired;

    public HollowRobesFireState(HollowRobes enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.robes = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        robes.StateMachine.ChangeState(robes.FloatState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        robes.FireProjectiles();
        hasFired = true;
    }

    public override void Enter()
    {
        base.Enter();
        hasFired = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!hasFired)
        {
            robes.CheckIfShouldFlip();
        }

        if (!robes.EnemyEntity.InKnockback)
        {
            robes.SetVelocityX(0);
            robes.SetVelocityY(0);
        }
        else
        {
            robes.SetVelocityX(robes.EnemyEntity.Knockback);
        }
    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        robes.AudioSource.PlayOneShot(robes.magicCircle,0.5f);
    }
}
