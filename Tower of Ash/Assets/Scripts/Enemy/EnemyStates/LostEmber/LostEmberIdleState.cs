using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostEmberIdleState : EnemyState {

    LostEmber ember;

    float timer;
    float maxTimer = 4f;

    public LostEmberIdleState(LostEmber enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.ember = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        timer = maxTimer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            if (ember.CheckIfPlayerInAggro())
            {
                ember.StateMachine.ChangeState(ember.FireState);
            }
        }

    }
}
