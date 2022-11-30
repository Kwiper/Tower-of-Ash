using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public EnemyStateMachine StateMachine { get; protected set; }

    #region Data
    [SerializeField]
    protected CombatData combatData;

    protected GameObject player;

    #endregion

    #region Components
    public Rigidbody2D RB { get; protected set; }
    public Animator Anim { get; protected set; }
    public SpriteRenderer Renderer { get; protected set; }
    public Entity EnemyEntity;
    #endregion

    #region Other variables
    public Vector2 CurrentVelocity { get; protected set; }
    public int FacingDirection { get; protected set; }
    protected Vector2 workspace;

    public PlayerData playerData;
    public int tinderReward;

    [SerializeField]
    Material whiteMat;

    [SerializeField]
    Material defaultMat;

    bool flashWhite;
    float flashTimer = 0.3f;

    #endregion

    #region Unity Callback Functions

    public virtual void Awake()
    {
        StateMachine = new EnemyStateMachine();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        EnemyEntity = GetComponent<Entity>();
        Renderer = GetComponent<SpriteRenderer>();
        FacingDirection = 1;

    }

    // Update is called once per frame
    public virtual void Update()
    {
        CurrentVelocity = RB.velocity;

        if (EnemyEntity.InKnockback) 
        {
            flashWhite = true;
            flashTimer = 0.3f;
        }

        if (flashWhite)
        {
            flashTimer -= Time.deltaTime;
            Renderer.material = whiteMat;

            if(flashTimer <= 0)
            {
                flashWhite = false;
            }
        }
        else
        {
            Renderer.material = defaultMat;
        }



        //StateMachine.CurrentState.LogicUpdate(); //Re-add this when states are added to this enemy

        if(EnemyEntity.Health <= 0)
        {
            playerData.tinder += tinderReward;
            Destroy(gameObject);
        }

    }

    public virtual void FixedUpdate()
    {
        //StateMachine.CurrentState.PhysicsUpdate(); // Re-add this when states are added to test enemy
    }

    // Hit stop on player
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if (!collider.gameObject.GetComponentInParent<Player>().invincible)
            {
                collider.gameObject.GetComponentInParent<Entity>().SetDamage(10);
                collider.gameObject.GetComponentInParent<Entity>().SetKnockback(-collider.gameObject.GetComponentInParent<Player>().FacingDirection);
                collider.gameObject.GetComponentInParent<Player>().isHit = true;
                collider.gameObject.GetComponentInParent<TimeStop>().StopTime(0.05f, 10, 0.2f);
            }
        }
    }

    #endregion

    #region Set Functions

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion

    #region Other Functions
    public void CheckIfShouldFlip()
    {
        int dir = 1;

        if (player.gameObject.transform.position.x <= gameObject.transform.position.x)
            dir = -1;
        else
            dir = 1;

        if (FacingDirection != dir)
            Flip();

    }

    protected virtual void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    protected virtual void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

}
