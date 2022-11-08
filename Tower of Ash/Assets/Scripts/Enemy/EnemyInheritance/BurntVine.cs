using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurntVine : Enemy
{

    public BurntVineIdleState IdleState { get; private set; }
    public BurntVineSpitState SpitState { get; private set; }
    public BurntVineSwipeState SwipeState { get; private set; }

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



}
