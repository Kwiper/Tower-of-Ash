using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpAttackState : EnemyState {
    public BossJumpAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
}
