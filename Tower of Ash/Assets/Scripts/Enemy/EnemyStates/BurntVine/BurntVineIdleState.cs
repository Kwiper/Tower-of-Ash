using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurntVineIdleState : EnemyState
{
    BurntVine vine;

    GameObject player;

    private float timer;
    private float maxTimer = 3f;

    public BurntVineIdleState(BurntVine enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.vine = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = FindObjectOfType<Player>().gameObject;

        timer = maxTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer -= Time.deltaTime;

        vine.CheckIfShouldFlip();

        if(timer <= 0)
        {
            if (player.gameObject.transform.position.y >= vine.gameObject.transform.position.y - 1)
            {
                if (vine.CheckIfPlayerInProjectileRange())
                {
                    vine.StateMachine.ChangeState(vine.SpitState);
                }
                else if (vine.CheckIfPlayerInSwipeRange())
                {
                    vine.StateMachine.ChangeState(vine.SwipeState);
                }
            }
        }
    }
}
