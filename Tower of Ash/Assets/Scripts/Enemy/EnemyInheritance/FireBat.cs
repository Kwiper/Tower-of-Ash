using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBat : Enemy {

    public FireBatIdleState IdleState { get; private set; }
    public FireBatAggroState AggroState { get; private set; }

    public AudioClip squeak;

    public Transform aggroPoint;
    [SerializeField]
    private float aggroRadius;

    [SerializeField]
    private LayerMask playerLayer;

    public int speed;

    public override void Awake()
    {
        base.Awake();

        IdleState = new FireBatIdleState(this, StateMachine, "idle");
        AggroState = new FireBatAggroState(this, StateMachine, "aggro");
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

        StateMachine.Initialize(IdleState);
    }

    public override void Update()
    {
        base.Update();

        StateMachine.CurrentState.LogicUpdate();
    }

    public bool CheckIfPlayerInAggroRange()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, aggroRadius, playerLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aggroPoint.position, aggroRadius);
    }
}
