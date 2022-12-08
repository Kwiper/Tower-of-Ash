using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFallState : EnemyState {

    Boss boss;

    int bossTriggerCounter;

    bool canFall;

    public BossFallState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
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

        switch (bossTriggerCounter)
        {
            case 0:
                canFall = true;
                break;
            case 1:
                boss.CastFallPillar();
                break;
        }

        bossTriggerCounter += 1;
    }

    public override void Enter()
    {
        base.Enter();

        canFall = false;

        bossTriggerCounter = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!canFall)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.bulletHellPoint.position, Time.deltaTime * 30);
        }

        if(canFall && !boss.CheckIfTouchingGround())
        {
            boss.SetVelocityY(-70);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SoundEffectTrigger()
    {
        base.SoundEffectTrigger();
        boss.AudioSource.PlayOneShot(boss.fall);
    }
}
