using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public BossIdleState IdleState { get; private set; }
    public BossWalkState WalkState { get; private set; }
    public BossPillarState PillarState { get; private set; }
    public BossLungeState LungeState { get; private set; }
    public BossJumpAttackState JumpAttackState { get; private set; }
    public BossFireballState FireballState { get; private set; }
    public BossBulletHellState BulletHellState { get; private set; }
    public BossFallState FallState { get; private set; }

    public override void Awake()
    {
        base.Awake();
        IdleState = new BossIdleState(this, StateMachine, "idle");
        WalkState = new BossWalkState(this, StateMachine, "walk");
        PillarState = new BossPillarState(this, StateMachine, "pillar");
        LungeState = new BossLungeState(this, StateMachine, "lunge");
        JumpAttackState = new BossJumpAttackState(this, StateMachine, "jumpAttack");
        FireballState = new BossFireballState(this, StateMachine, "fireball");
        BulletHellState = new BossBulletHellState(this, StateMachine, "bulletHell");
        FallState = new BossFallState(this, StateMachine, "fall");

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        StateMachine.CurrentState.PhysicsUpdate();
    }

    public override void Start()
    {
        base.Start();
        FacingDirection = -1;
        StateMachine.Initialize(IdleState);
    }

    public override void Update()
    {
        base.Update();
        StateMachine.CurrentState.LogicUpdate();

        
    }

}
