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
    public AudioSource AudioSource { get; protected set; }

    #endregion

    #region Other variables
    public Vector2 CurrentVelocity { get; protected set; }
    public int FacingDirection { get; protected set; }
    protected Vector2 workspace;
    [SerializeField]
    private bool doesNotMove = false;
    [SerializeField]
    private bool isMoving = false;   

    public PlayerData playerData;
    public int tinderReward;

    [SerializeField]
    List<GameObject> tinderList;

    [SerializeField]
    GameObject[] tinderObjects;

    [SerializeField]
    Material whiteMat;

    [SerializeField]
    Material defaultMat;

    bool flashWhite;
    float flashTimer = 0.2f;
    float deathTimer = 1f;

    #endregion

    #region Particles
    [SerializeField] GameObject hitParticleContainer;
    [SerializeField] GameObject deathParticleContainer;
    bool triggerParticles;
    #endregion

    #region Sound Effects
    public AudioClip hit;
    public AudioClip death;

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
        AudioSource = GetComponent<AudioSource>();

        FacingDirection = 1;
        RB.constraints = RigidbodyConstraints2D.FreezePosition;
        CreateTinderList();

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(doesNotMove == false && isMoving == false){
            //Freeze rotation, toggles the z-constraint boolean but removes the x and y constraint boolean
            RB.constraints = RigidbodyConstraints2D.FreezeRotation;
            isMoving = true;
        }

        CurrentVelocity = RB.velocity;

        if (EnemyEntity.InKnockback) 
        {
            flashWhite = true;
            flashTimer = 0.2f;

            if(triggerParticles){
                // Trigger particles
                GameObject hitParticle = Instantiate(hitParticleContainer, transform);
                hitParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                hitParticle.GetComponent<ParticleSystem>().Play();
                Destroy(hitParticle, 1f);
                AudioSource.PlayOneShot(hit);
                triggerParticles = false;
            }
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
            triggerParticles = true; //reset particle trigger
        }



        //StateMachine.CurrentState.LogicUpdate(); //Re-add this when states are added to this enemy

        if(EnemyEntity.Health <= 0)
        {
            //disable knockback

            if(deathTimer == 1f){
                SpawnTinder();
                AudioSource.PlayOneShot(death);
                //disable renderer and collider
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;

                //trigger particle
                GameObject deathParticle = Instantiate(deathParticleContainer, transform);
                deathParticle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                deathParticle.GetComponent<ParticleSystem>().Play();
                Destroy(deathParticle, 2f);
            }

            if(deathTimer <= 0f) {
                Destroy(gameObject);
            }
            else deathTimer -= Time.deltaTime;
            
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

    protected virtual void SoundEffectTrigger() => StateMachine.CurrentState.SoundEffectTrigger();

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

    void CreateTinderList()
    {
        int tens = tinderReward / 10;
        for (int i = 0; i < tens; i++)
        {
            tinderList.Add(tinderObjects[2]);
        }

        int fives = (tinderReward % 10) / 5;
        for (int i = 0; i < fives; i++)
        {
            tinderList.Add(tinderObjects[1]);
        }

        int ones = (tinderReward % 10) % 5;
        for (int i = 0; i < ones; i++)
        {
            tinderList.Add(tinderObjects[0]);
        }
    }

    void SpawnTinder()
    {
        for (int i = 0; i < tinderList.Count; i++)
        {
            Vector2 randomVel = new Vector2(Random.Range(-1f, 1f), 1).normalized;

            GameObject instance = Instantiate(tinderList[i], transform.position, transform.rotation);
            instance.GetComponent<Rigidbody2D>().velocity = randomVel;

        }
    }


}
