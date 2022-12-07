using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostEmber : Enemy
{
    public LostEmberIdleState IdleState { get; private set; }
    public LostEmberFireState FireState { get; private set; }

    public AudioClip splat;

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

    //private Vector3 facingRight = Vector3(0,0,-90);
    //private Vector3 facingleft = Vector3(0,0,90);
    //private Vector3 facingUp = Vector3(0,0,0);
    //private Vector3 facingDown = Vector3(0,0,180);

    private Vector2 direction;    
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
        //Hard coded these numbers because Quaternions are fucking stupid
        if(GetComponent<Transform>().rotation.z == 0){
            direction = Vector2.up;
        }
        else if(GetComponent<Transform>().rotation.z == -1){
            direction = Vector2.down;
        }
        else if(GetComponent<Transform>().rotation.z == -0.7071068f){
            direction = Vector2.right;
        }
        else if(GetComponent<Transform>().rotation.z == 0.7071068f){
            direction = Vector2.left;
        }

        GameObject instance = Instantiate(EmberProjectile, firePoint.transform.position, firePoint.transform.rotation);

        instance.GetComponent<EmberProjectile>().angle = direction + new Vector2(randomDirection, 0);

        AudioSource.PlayOneShot(splat);
    }

        private void OnDrawGizmos()
    {
        //Aggro Radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector2(aggroPoint.position.x,aggroPoint.position.y), aggroRadius);

    
    }

}
