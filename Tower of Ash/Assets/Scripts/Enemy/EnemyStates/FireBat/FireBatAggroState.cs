using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBatAggroState : EnemyState {

    FireBat bat;

    private Vector2 direction;
    private GameObject player;

    public FireBatAggroState(FireBat enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.bat = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = FindObjectOfType<Player>().gameObject;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        bat.CheckIfShouldFlip();

        direction = new Vector2(player.transform.position.x - bat.transform.position.x, player.transform.position.y - bat.transform.position.y).normalized;

        if (!bat.EnemyEntity.InKnockback)
        {
            bat.SetVelocity(bat.speed,direction);            
        }
        else
        {
            bat.SetVelocityX(bat.EnemyEntity.Knockback);
        }
    }
}
