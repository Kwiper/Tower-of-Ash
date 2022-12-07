using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurntVine : Enemy
{

    public BurntVineIdleState IdleState { get; private set; }
    public BurntVineSpitState SpitState { get; private set; }
    public BurntVineSwipeState SwipeState { get; private set; }

    public AudioClip swipe;
    public AudioClip spit;

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    Transform aggroPoint;

    public float projectileRange;
    public float swipeRange;

    [SerializeField]
    GameObject AshProjectile;

    [SerializeField]
    Transform spitPoint;

    public override void Awake()
    {
        base.Awake();

        IdleState = new BurntVineIdleState(this, StateMachine, "idle");
        SpitState = new BurntVineSpitState(this, StateMachine, "spit");
        SwipeState = new BurntVineSwipeState(this, StateMachine, "swipe");
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

    public bool CheckIfPlayerInSwipeRange()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, swipeRange, playerLayer);
    }

    public bool CheckIfPlayerInProjectileRange()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, projectileRange, playerLayer) && !CheckIfPlayerInSwipeRange();
    }

    public void SpitAsh()
    {
        GameObject instance = Instantiate(AshProjectile, spitPoint.transform.position, spitPoint.transform.rotation) as GameObject;
        instance.GetComponent<AshProjectile>().direction = FacingDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(aggroPoint.position, projectileRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aggroPoint.position, swipeRange);
    }

}
