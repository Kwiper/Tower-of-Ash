using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Hogash : Enemy
{
    public HogashWalkState WalkState { get; private set; }
    public HogashRunState RunState { get; private set; }
    public HogashChargeState ChargeState { get; private set; }
    public HogashStop StopState { get; private set; }


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
        WalkState = new HogashWalkState(this, StateMachine, "walk");
        RunState = new HogashRunState(this, StateMachine, "run");
        ChargeState = new HogashChargeState(this, StateMachine, "chargeUp");
        StopState = new HogashStop(this, StateMachine, "stop");
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
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f,groundLayer);
    }

    public bool CheckIfPlayerInAggro()
    {
        return Physics2D.Raycast(aggroPoint.position, Vector2.right * FacingDirection, 5,playerLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(aggroPoint.position, (Vector2.right * FacingDirection) * 5);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ledgeCheck.position, 0.1f);
        Gizmos.DrawWireSphere(wallCheck.position, 0.1f);
    }
}
