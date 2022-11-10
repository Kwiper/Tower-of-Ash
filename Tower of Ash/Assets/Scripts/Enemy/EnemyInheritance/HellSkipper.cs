using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSkipper : Enemy
{

    public HellSkipperWalkState WalkState { get; private set;}
    public HellSkipperThrustState ThrustState { get; private set; }
    public HellSkipperJumpState JumpState { get; private set; }
    public HellSkipperDownState DownState { get; private set; }

    [SerializeField]
    private Transform ledgeCheck;

    [SerializeField]
    private Transform wallCheck;

    [SerializeField]
    private Transform aggroPoint;

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float aggroRange;


    public override void Awake()
    {
        base.Awake();

        WalkState = new HellSkipperWalkState(this, StateMachine, "walk");
        ThrustState = new HellSkipperThrustState(this, StateMachine, "thrust");
        JumpState = new HellSkipperJumpState(this, StateMachine, "jump");
        DownState = new HellSkipperDownState(this, StateMachine, "down");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public override void Start()
    {
        base.Start();
        FacingDirection = 1;

        StateMachine.Initialize(WalkState);

    }

    public override void Update()
    {
        base.Update();

        StateMachine.CurrentState.LogicUpdate();
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.OverlapCircle(ledgeCheck.position, 0.1f, groundLayer);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
    }

    public bool CheckIfPlayerInAggro()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, aggroRange, playerLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aggroPoint.position, aggroRange);

    }
}
