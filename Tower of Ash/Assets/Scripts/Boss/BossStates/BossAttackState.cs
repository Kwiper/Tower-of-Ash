using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : EnemyState {
    public BossAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
}
