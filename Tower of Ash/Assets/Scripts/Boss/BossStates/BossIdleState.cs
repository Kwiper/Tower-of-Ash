using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : EnemyState
{

    private float maxTimer = 2f;
    private float timer;
    private Boss boss;
    private string animBoolName;

    private int randomInt;

    private GameObject player;

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
        player = FindObjectOfType<Player>().gameObject;

        timer = maxTimer;
    }

    public override void Exit()
    {
        base.Exit();
        boss.SetVelocityX(0);
        boss.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        boss.CheckIfShouldFlip();
        timer -= Time.deltaTime;


        if(timer <= 0)
        {
            // HP range 75% - 100%            
            if(boss.EnemyEntity.Health > (boss.EnemyEntity.maxHealth * 0.75)) 
            {
                if (!boss.CheckIfPlayerInAggroRange() && boss.CheckIfPlayerInProjectileRadius()) // If player is outside aggro range, inside projectile radius
                {
                    boss.StateMachine.ChangeState(boss.WalkState); // Switch to walk state
                }
                else if (boss.CheckIfPlayerInAggroRange()) // If player is in aggro range
                {
                    boss.StateMachine.ChangeState(boss.AttackState); // Switch to attack state
                }
                else if (!boss.CheckIfPlayerInProjectileRadius()) // If player is outside projectile range
                {
                    boss.StateMachine.ChangeState(boss.LungeState); // Lunge
                }
            }

            // HP range 50% - 75%
            else if(boss.EnemyEntity.Health <= (boss.EnemyEntity.maxHealth) && boss.EnemyEntity.Health > (boss.EnemyEntity.maxHealth / 2))
            {
                randomInt = Random.Range(1, 4);

                if (!boss.CheckIfPlayerInAggroRange() && boss.CheckIfPlayerInProjectileRadius()) // If player is outside aggro range, inside projectile radius
                {
                    switch (randomInt)
                    {
                        case 1:
                            boss.StateMachine.ChangeState(boss.WalkState); // Switch to walk state
                            break;

                        case 2:
                            if (player.transform.position.y > boss.transform.position.y + 1) // If player is above the boss
                            {
                                boss.StateMachine.ChangeState(boss.JumpAttackState); // Switch to jump attack state
                            }
                            else
                            {
                                boss.StateMachine.ChangeState(boss.FireballState); // Switch to fireball state
                            }
                            break;
                    }                    
                }

                else if (boss.CheckIfPlayerInAggroRange()) // If player is in aggro range
                {
                    if (player.transform.position.y > boss.transform.position.y + 1) // If player is above the boss
                    {
                        switch (randomInt)
                        {
                            case 1:
                                boss.StateMachine.ChangeState(boss.JumpAttackState); // Switch to jump attack state
                                break;
                            case 2:
                                boss.StateMachine.ChangeState(boss.AttackState); // Switch to attack state
                                break;
                        }
                    }
                    else
                    {
                        boss.StateMachine.ChangeState(boss.AttackState); // Switch to attack state
                    }
                }

                else if (!boss.CheckIfPlayerInProjectileRadius()) // If player is outside projectile range
                {
                    boss.StateMachine.ChangeState(boss.LungeState); // Lunge
                }
            } 

            // HP range 25% - 50%
            else if(boss.EnemyEntity.Health <= (boss.EnemyEntity.maxHealth / 2) && boss.EnemyEntity.Health > (boss.EnemyEntity.maxHealth * 0.25))
            {
                if (!boss.CheckIfPlayerInAggroRange() && boss.CheckIfPlayerInProjectileRadius()) // If player is outside aggro range, inside projectile radius
                {
                    randomInt = Random.Range(1, 4);

                    switch (randomInt)
                    {
                        case 1:
                            boss.StateMachine.ChangeState(boss.WalkState); // Switch to walk state
                            break;

                        case 2:
                            if (player.transform.position.y > boss.transform.position.y + 1) // If player is above the boss
                            {
                                boss.StateMachine.ChangeState(boss.JumpAttackState); // Switch to jump attack state
                            }
                            else
                            {
                                boss.StateMachine.ChangeState(boss.FireballState); // Switch to fireball state
                            }
                            break;
                        case 3:
                            boss.StateMachine.ChangeState(boss.PillarState);
                            break;
                    }
                }

                else if (boss.CheckIfPlayerInAggroRange()) // If player is in aggro range
                {
                    randomInt = Random.Range(1, 3);
                    if (player.transform.position.y > boss.transform.position.y + 1) // If player is above the boss
                    {
                        switch (randomInt)
                        {
                            case 1:
                                boss.StateMachine.ChangeState(boss.JumpAttackState); // Switch to jump attack state
                                break;
                            case 2:
                                boss.StateMachine.ChangeState(boss.AttackState); // Switch to attack state
                                break;
                        }
                    }
                    else
                    {
                        boss.StateMachine.ChangeState(boss.AttackState); // Switch to attack state
                    }
                }

                else if (!boss.CheckIfPlayerInProjectileRadius()) // If player is outside projectile range
                {
                    boss.StateMachine.ChangeState(boss.LungeState); // Lunge
                }
            }

            // HP range 0% - 25%
            else if(boss.EnemyEntity.Health <= (boss.EnemyEntity.maxHealth * 0.25))
            {
                maxTimer = 1f;
                Debug.Log(boss.canBulletHell);

                if (boss.canBulletHell)
                {
                    boss.StateMachine.ChangeState(boss.BulletHellCharge);
                }
                else if (!boss.canBulletHell)
                {
                    if (!boss.CheckIfPlayerInAggroRange() && boss.CheckIfPlayerInProjectileRadius()) // If player is outside aggro range, inside projectile radius
                    {
                        randomInt = Random.Range(1, 4);

                        switch (randomInt)
                        {
                            case 1:
                                boss.StateMachine.ChangeState(boss.WalkState); // Switch to walk state
                                break;

                            case 2:
                                if (player.transform.position.y > boss.transform.position.y + 1) // If player is above the boss
                                {
                                    boss.StateMachine.ChangeState(boss.JumpAttackState); // Switch to jump attack state
                                }
                                else
                                {
                                    boss.StateMachine.ChangeState(boss.FireballState); // Switch to fireball state
                                }
                                break;
                            case 3:
                                boss.StateMachine.ChangeState(boss.PillarState);
                                break;
                        }
                    }

                    else if (boss.CheckIfPlayerInAggroRange()) // If player is in aggro range
                    {
                        randomInt = Random.Range(1, 3);
                        if (player.transform.position.y > boss.transform.position.y + 1) // If player is above the boss
                        {
                            switch (randomInt)
                            {
                                case 1:
                                    boss.StateMachine.ChangeState(boss.JumpAttackState); // Switch to jump attack state
                                    break;
                                case 2:
                                    boss.StateMachine.ChangeState(boss.AttackState); // Switch to attack state
                                    break;
                            }
                        }
                        else
                        {
                            boss.StateMachine.ChangeState(boss.AttackState); // Switch to attack state
                        }
                    }

                    else if (!boss.CheckIfPlayerInProjectileRadius()) // If player is outside projectile range
                    {
                        boss.StateMachine.ChangeState(boss.LungeState); // Lunge
                    }
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
