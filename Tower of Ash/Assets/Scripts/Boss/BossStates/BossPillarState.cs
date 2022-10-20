using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPillarState : EnemyState {
    public BossPillarState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
}
