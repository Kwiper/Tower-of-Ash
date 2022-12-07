using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class BlackGolem : Enemy {

    public BlackGolemWalkState WalkState { get; private set;}
    public BlackGolemSlamState SlamState { get; private set; }
    public BlackGolemChargeState ChargeState { get; private set; }
    public BlackGolemRunState RunState { get; private set; }

    public AudioClip step;
    public AudioClip slam;

    [SerializeField]
    Transform wallCheck;
    [SerializeField]
    Transform ledgeCheck;

    [SerializeField]
    Transform chargePoint;
    [SerializeField]
    float chargeRadius;
    [SerializeField]
    Transform slamPoint;
    [SerializeField]
    float slamRadius;

    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Transform aggroPoint;
    [SerializeField]
    float aggroRadius;

    [SerializeField]
    GameObject FirePillar;

    public override void Awake()
    {
        base.Awake();

        WalkState = new BlackGolemWalkState(this, StateMachine, "walk");
        SlamState = new BlackGolemSlamState(this, StateMachine, "slam");
        ChargeState = new BlackGolemChargeState(this, StateMachine, "charge");
        RunState = new BlackGolemRunState(this, StateMachine, "run");
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

    public bool CheckIfPlayerInChargeRadius()
    {
        return Physics2D.OverlapCircle(chargePoint.position, chargeRadius, playerLayer);
    }

    public bool CheckIfPlayerInSlamRadius()
    {
        return Physics2D.OverlapCircle(slamPoint.position, slamRadius, playerLayer);
    }

    public bool CheckIfPlayerInAggroRadius()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, aggroRadius, playerLayer);
    }

    public void CastFirePillar(float distance)
    {
        GameObject instance = Instantiate(FirePillar, new Vector2(this.transform.position.x + distance * FacingDirection, this.transform.position.y -1.18f), this.transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(chargePoint.position, chargeRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(slamPoint.position, slamRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(aggroPoint.position, aggroRadius);
    }

}
