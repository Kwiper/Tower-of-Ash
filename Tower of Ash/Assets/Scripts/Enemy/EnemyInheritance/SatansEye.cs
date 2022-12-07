using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatansEye : Enemy
{

    public SatansEyeFlyState FlyState { get; private set; }
    public SatansEyeFireState FireState { get; private set; }

    public AudioClip fireball;

    public Transform aggroPoint;
    [SerializeField]
    private float aggroRadius;
    [SerializeField]
    private float playerRadius;

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    GameObject EyeProjectile;

    [SerializeField]
    Transform firePoint;

    public override void Awake()
    {
        base.Awake();
        FlyState = new SatansEyeFlyState(this, StateMachine, "fly");
        FireState = new SatansEyeFireState(this, StateMachine, "fire");
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

        StateMachine.Initialize(FlyState);
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

    public void Fire()
    {
        AudioSource.PlayOneShot(fireball);
        GameObject instance = Instantiate(EyeProjectile, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
        instance.GetComponent<EyeProjectile>().direction = FacingDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aggroPoint.position, aggroRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(aggroPoint.position, playerRadius);
    }

}
