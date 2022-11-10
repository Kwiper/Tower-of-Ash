using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AshenBelcher : Enemy {

    public AshenBelcherWalk WalkState { get; private set; }
    public AshenBelcherBelch BelchState { get; private set; }
    public AshenBelcherFlee FleeState { get; private set; }

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
    private float aggroRadius;

    [SerializeField]
    GameObject BelchProjectile;

    [SerializeField]
    private Transform firePoint;

    public override void Awake()
    {
        base.Awake();
        WalkState = new AshenBelcherWalk(this, StateMachine, "walk");
        BelchState = new AshenBelcherBelch(this, StateMachine, "belch");
        FleeState = new AshenBelcherFlee(this, StateMachine, "flee");

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
        return Physics2D.OverlapCircle(aggroPoint.position, aggroRadius, playerLayer);
    }

    public void Belch()
    {
        int angleX = 3;
        int angleY = 0;

        for(int i = 0; i < 3; i++)
        {
            GameObject instance = Instantiate(BelchProjectile, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
            instance.GetComponent<BelchProjectile>().angle = new Vector2(angleX * FacingDirection, angleY);
            instance.GetComponent<BelchProjectile>().direction = FacingDirection;

            angleX -= 1;
            angleY += 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(aggroPoint.position, aggroRadius);
    }

}


