using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public BossIdleState IdleState { get; private set; }
    public BossAttackState AttackState { get; private set; }
    public BossWalkState WalkState { get; private set; }
    public BossPillarState PillarState { get; private set; }
    public BossLungeState LungeState { get; private set; }
    public BossJumpAttackState JumpAttackState { get; private set; }
    public BossFireballState FireballState { get; private set; }
    public BossBulletHellState BulletHellState { get; private set; }
    public BossFallState FallState { get; private set; }

    [SerializeField]
    private Transform AggroPoint;
    public LayerMask playerLayer;

    [SerializeField]
    private Transform BoundaryPoint;
    public LayerMask groundLayer;

    [SerializeField]
    private float AggroRadius;

    [SerializeField]
    private float ProjectileRadius;

    public override void Awake()
    {
        base.Awake();
        IdleState = new BossIdleState(this, StateMachine, "idle");
        AttackState = new BossAttackState(this, StateMachine, "attack");
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

    public bool CheckIfPlayerInAggroRange()
    {
        return Physics2D.OverlapCircle(AggroPoint.position, AggroRadius, playerLayer);
    }

    public bool CheckIfPlayerInProjectileRadius()
    {
        return Physics2D.OverlapCircle(AggroPoint.position, ProjectileRadius, playerLayer);
    }

    public bool CheckIfBoundaryDetected()
    {
        return Physics2D.OverlapCircle(BoundaryPoint.position, 0.5f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AggroPoint.position, AggroRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(AggroPoint.position, ProjectileRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(BoundaryPoint.position, 0.5f);
    }

}
