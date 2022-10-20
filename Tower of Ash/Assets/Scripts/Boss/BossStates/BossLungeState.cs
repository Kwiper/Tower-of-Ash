using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLungeState : EnemyState {
    public BossLungeState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
}
