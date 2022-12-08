using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRegalState : EnemyState
{

    private Boss boss;

    public BossRegalState(Boss enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        boss = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (BossEventManager.bossFight)
        {
            boss.StateMachine.ChangeState(boss.IdleState);
        }
    }
}
