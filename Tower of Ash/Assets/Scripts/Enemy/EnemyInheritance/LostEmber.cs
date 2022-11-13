using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostEmber : Enemy
{
    public LostEmberIdleState IdleState { get; private set; }
    public LostEmberFireState FireState { get; private set; }

    [SerializeField]
    Transform aggroPoint;
    [SerializeField]
    float aggroRadius;

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    Transform firePoint;

    [SerializeField]
    GameObject EmberProjectile;

    public override void Awake()
    {
        base.Awake();
        IdleState = new LostEmberIdleState(this, StateMachine, "idle");
        FireState = new LostEmberFireState(this, StateMachine, "fire");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    public override void Update()
    {
        base.Update();

        StateMachine.CurrentState.LogicUpdate();
    }

    public bool CheckIfPlayerInAggro()
    {
        return Physics2D.OverlapCircle(aggroPoint.position, aggroRadius, playerLayer);
    }

    public void FireEmber()
    {
        float randomDirection = Random.Range(-0.5f, 0.5f);

        GameObject instance = Instantiate(EmberProjectile, firePoint.transform.position, firePoint.transform.rotation);
        instance.GetComponent<EmberProjectile>().angle = Vector2.up + new Vector2(randomDirection, 0);
    }

}
