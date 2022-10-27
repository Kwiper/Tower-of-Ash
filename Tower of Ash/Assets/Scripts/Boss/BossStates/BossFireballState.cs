using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireballState : EnemyState {

    private Boss boss;
    public BossFireballState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
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

        // Shoot fireball
        Debug.Log("Fireball!");
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }
}
