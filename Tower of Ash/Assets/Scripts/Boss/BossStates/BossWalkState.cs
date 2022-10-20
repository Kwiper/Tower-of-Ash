using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalkState : EnemyState {
    public BossWalkState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }


}
