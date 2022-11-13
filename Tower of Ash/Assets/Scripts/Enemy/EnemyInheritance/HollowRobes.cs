using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowRobes : Enemy
{

    public HollowRobesFloatState FloatState { get; private set; }
    public HollowRobesFireState FireState { get; private set; }

    [SerializeField]
    Transform aggroPoint;

    [SerializeField]
    float aggroRadius;

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    GameObject RobesProjectile;

    [SerializeField]
    float playerRadius;

    [SerializeField]
    float fleeRadius;

    [SerializeField]
    Transform firePoint;

    public override void Awake()
    {
        base.Awake();
        FloatState = new HollowRobesFloatState(this, StateMachine, "float");
        FireState = new HollowRobesFireState(this, StateMachine, "fire");
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

    public bool CheckIfPlayerInAggro()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, aggroRadius, playerLayer) && !CheckIfInPlayerRadius();
    }

    public bool CheckIfInPlayerRadius()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, playerRadius, playerLayer);
    }

    public bool CheckIfInFleeRadius()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, fleeRadius, playerLayer);
    }

    public void FireProjectiles()
    {
        float angleX = 0.5f;
        float angleY = -0.5f;

        for(int i = 0; i < 3; i++)
        {
            GameObject instance = Instantiate(RobesProjectile, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
            instance.GetComponent<RobesProjectile>().projectileDirection = new Vector2(angleX * FacingDirection, angleY).normalized;

            if (i == 0)
            {
                angleX += 0.5f;
            }
            if(i == 1)
            {
                angleX -= 0.5f;
            }
            angleY += 0.5f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(aggroPoint.position, aggroRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(aggroPoint.position, playerRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aggroPoint.position, fleeRadius);

    }

}
