using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class WailingPhantom : Enemy
{
    public WailingPhantomFloatState FloatState { get; private set; }
    public WailingPhantomChargeState ChargeState { get; private set; }
    public WailingPhantomAttackState AttackState { get; private set; }

    public AudioClip dash;

    public Transform aggroPoint;
    [SerializeField]
    private float aggroRadius;
    [SerializeField]
    private float playerRadius;

    [SerializeField]
    private LayerMask playerLayer;

    public override void Awake()
    {
        FloatState = new WailingPhantomFloatState(this, StateMachine, "float");
        ChargeState = new WailingPhantomChargeState(this, StateMachine, "charge");
        AttackState = new WailingPhantomAttackState(this, StateMachine, "attack");

        base.Awake();
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

        StateMachine.Initialize(FloatState);
    }

    public override void Update()
    {
        base.Update();

        StateMachine.CurrentState.LogicUpdate();
    }

    public bool CheckIfPlayerInAggroRange()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, aggroRadius, playerLayer) && !CheckIfNearPlayer();
    }

    public bool CheckIfNearPlayer()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, playerRadius, playerLayer);
    }
}
