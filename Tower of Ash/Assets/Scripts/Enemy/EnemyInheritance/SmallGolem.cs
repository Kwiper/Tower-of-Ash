using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGolem : Enemy
{

    public SmallGolemWalkState WalkState { get; private set; }
    public SmallGolemThrowState ThrowState { get; private set; }

    public AudioClip walk;
    public AudioClip throwRock;

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

    [SerializeField]
    private GameObject Rock;

    [SerializeField]
    private Transform throwPoint;


    public override void Awake()
    {
        base.Awake();
        WalkState = new SmallGolemWalkState(this, StateMachine, "walk");
        ThrowState = new SmallGolemThrowState(this, StateMachine, "throw");
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

    public bool CheckIfPlayerInAggroRange()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, aggroRange, playerLayer);
    }

    public void ThrowRock()
    {
        AudioSource.PlayOneShot(throwRock);
        GameObject instance = Instantiate(Rock, throwPoint.transform.position, throwPoint.transform.rotation) as GameObject;
        instance.GetComponent<Rock>().angle = new Vector2(1 * FacingDirection, 3);
    }
}
