using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurntVineIdleState : EnemyState
{
    BurntVine vine;

    public BurntVineIdleState(BurntVine enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.vine = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        vine.CheckIfShouldFlip();
    }
}
