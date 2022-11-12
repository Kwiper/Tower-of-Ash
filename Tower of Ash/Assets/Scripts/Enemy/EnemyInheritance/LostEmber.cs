using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostEmber : Enemy
{
    public override void Awake()
    {
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

    }

    public override void Update()
    {
        base.Update();

        StateMachine.CurrentState.LogicUpdate();
    }
}
