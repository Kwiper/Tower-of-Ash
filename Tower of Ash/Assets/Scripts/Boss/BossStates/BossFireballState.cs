using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireballState : EnemyState {
    public BossFireballState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
}
