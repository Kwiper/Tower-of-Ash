using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : EnemyState
{

    private float maxTimer = 2f;
    private float timer;
    private Boss boss;
    private string animBoolName;

    public BossIdleState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.boss = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        timer = maxTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.CheckIfShouldFlip();
        timer -= Time.deltaTime;

        Debug.Log(stateMachine);

        if(timer <= 0)
        {

        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
