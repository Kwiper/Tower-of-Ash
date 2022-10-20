using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFallState : EnemyState {
    public BossFallState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
}
