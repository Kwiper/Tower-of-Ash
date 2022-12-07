using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerFireballState FireballState { get; private set; }
    public PlayerChargeAttackState ChargeAttackState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerHealState HealState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private CombatData combatData;
    [SerializeField]
    private UpgradeData upgradeData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Entity PlayerEntity { get; private set; }
    public SpriteRenderer SpriteRenderer;
    public AudioSource AudioSource { get; private set; }
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ceilingCheck;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Transform cameraPoint;
    [SerializeField]
    private LayerMask portalLayer;
    [SerializeField]
    public bool stateDebug;
    [SerializeField]
    public Transform playerTransform;
    [SerializeField]
    public CinemachineConfiner camConfine;
    public bool isReal = false;
    public bool hitHead = false;
    public bool manualCheckPointSection = false;
    public bool hitSpike = false;
    public bool firstReload = false;
    public Vector2 spawnPoint = new Vector2(-2,-58);
    private Vector2 displaceVector;
    private float step;
    private float lookSpeed = 27.5f;
    private Vector2 lookPosDefault;
    private Vector2 spikeCheckPoint;
    private Collider2D newWorldBound;
    //public bool FreezePos;
    #endregion

    #region Other variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    private Vector2 workspace;


    public GameObject Fireball;

    public bool healthCanCountdown = true;

    public bool isHit = false;

    public bool invincible = false;

    public bool playedChargeSound = false;

    #endregion

    #region Scene Detail Variables
    public SceneDetails CurrentScene { get; private set;}
    public SceneDetails PrevScene { get; private set;}
    #endregion

    #region Particles
    [SerializeField] public GameObject hitParticleContainer;
    [SerializeField] public GameObject chargeParticleContainer;
    [SerializeField] public GameObject jumpParticleContainer;
    [SerializeField] public GameObject wallJumpParticleContainer;
    bool triggerParticles;
    #endregion

    #region Sound Effects

    public AudioClip sword1;
    public AudioClip sword2;
    public AudioClip sword3;
    public AudioClip swordBig;
    public AudioClip dash;

    public AudioClip footstep1;
    public AudioClip footstep2;
    public AudioClip footstep3;
    public AudioClip footstep4;

    public AudioClip jump;
    public AudioClip airJump;

    public AudioClip wallSlide;
    public AudioClip touchWall;

    public AudioClip chargeAttack;

    public AudioClip fireball;

    public AudioClip heal;
    public AudioClip cape1;
    public AudioClip cape2;
    public AudioClip cape3;
    public AudioClip cape4;

    public AudioClip hit;
    public AudioClip death;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");

        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");

        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");

        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");

        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");

        FireballState = new PlayerFireballState(this, StateMachine, playerData, "fireball");

        ChargeAttackState = new PlayerChargeAttackState(this, StateMachine, playerData, "chargeAttack");

        HitState = new PlayerHitState(this, StateMachine, playerData, "hit");

        HealState = new PlayerHealState(this, StateMachine, playerData, "heal");

        DeathState = new PlayerDeathState(this, StateMachine, playerData, "death");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        PlayerEntity = GetComponent<Entity>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();

        FacingDirection = 1;
        var camConfined = GameObject.Find("CM vcam1");
        camConfine = camConfined.GetComponent<CinemachineConfiner>();
        //var camConfine = GameObject.Find("CM vcam1").GetComponent<CinemachineConfiner2D>();
        //Debug.Log(camConfined);
        var pos = new Vector2(gameObject.transform.Find("CameraPoint").position.x,gameObject.transform.Find("CameraPoint").position.y);
        lookPosDefault = pos;
        ResetHealCharges();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        step = lookSpeed * Time.deltaTime;

        if(firstReload)firstReloadCamConfine();
        CurrentVelocity = RB.velocity;
        SetLookDisplacement();
        if(InputHandler.chargeHeld && playerData.unlockedChargeAttack){
            //SpriteRenderer.color = Color.gray;
            if (!playedChargeSound)
            {
                AudioSource.PlayOneShot(chargeAttack, 0.5f);
                playedChargeSound = true;
            }

            // Trigger particles
            GameObject cParticle = Instantiate(chargeParticleContainer, transform);
            cParticle.transform.position = new Vector3(transform.position.x, transform.position.y-.2f, transform.position.z);
            cParticle.GetComponent<ParticleSystem>().Play();
            Destroy(cParticle, 0.25f);
        }
        else{
            playedChargeSound = false;
        }

        StateMachine.CurrentState.LogicUpdate();

        HealthTimer();

        if (PlayerEntity.InKnockback)
        {
            SetVelocityX(PlayerEntity.Knockback);

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
        else triggerParticles = true;

        if (isHit)
        {
            StateMachine.ChangeState(HitState);
            invincible = true;
            StartCoroutine(Invincibility());
        }

        if (invincible)
        {
            StartCoroutine(SpriteFlicker());
        }
        else
        {
            SpriteRenderer.enabled = true;
        }


        // When HP reaches 0 from timer, switch to death state.
        if (PlayerEntity.Health <= 0 && StateMachine.CurrentState != HitState)
        {
            RB.constraints = RigidbodyConstraints2D.FreezePosition;
            StateMachine.ChangeState(DeathState);
        }
        //if(!manualCheckPointSection || !hitSpike)StartCoroutine(setCheckPointPosRay());
        if(!manualCheckPointSection || !manualCheckPointSection && !hitSpike)setCheckPointPosRay();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        OnMoveOver();
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

    public void SetLookDisplacement()
    {
        displaceVector = new Vector2(InputHandler.NormLookInputX + gameObject.GetComponent<Transform>().position.x, InputHandler.NormLookInputY + gameObject.GetComponent<Transform>().position.y + 1);

        cameraPoint.position = Vector2.MoveTowards(cameraPoint.position, displaceVector, step);
    }

    #endregion

    #region Check Functions
    public bool CheckIfGrounded()
    {
        return (Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround)) || (Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsPlatform));
        
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingCeiling()
    {
        return Physics2D.Raycast(ceilingCheck.position, Vector2.up , playerData.wallCheckDistance, playerData.whatIsGround);
    }
    public RaycastHit2D CheckCeilingType()
    {
        var hit = Physics2D.Raycast(ceilingCheck.position, Vector2.up , playerData.wallCheckDistance, playerData.whatIsGround);
        return hit;
    }
    public RaycastHit2D CheckFloorType()
    {
        var hitGround = Physics2D.Raycast(groundCheck.position, Vector2.down , playerData.wallCheckDistance, playerData.whatIsGround);


        var hitPlatform = Physics2D.Raycast(groundCheck.position, Vector2.down , playerData.wallCheckDistance, playerData.whatIsPlatform);
        //Debug.Log("Platform distance is "+ hitPlatform.distance + " Ground distance is "+ hitGround.distance);
        if(hitGround.distance > hitPlatform.distance){
            return hitGround;
        }
        else{
            //Debug.Log("Platform detected");
            return hitPlatform;
        }
    }

    public void setConfiner(Collider2D newWorldBound){
        camConfine.m_BoundingShape2D = newWorldBound;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection && !PauseMenu.GameIsPaused)
        {
            Flip();
        }
    }

    public void OnMoveOver()
    {

       var collider = Physics2D.OverlapCircle(transform.position - new Vector3(0,0),0.2f, portalLayer);
       if(collider != null){
            var triggerable = collider.GetComponent<IplayerTriggerable>();

       
            if(triggerable != null)
            {
                triggerable.OnPlayerTriggered(this);
            }
       }
    }
    
    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void SoundEffectTrigger() => StateMachine.CurrentState.SoundEffectTrigger();

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void CastFireball()
    {
        Instantiate(Fireball,firePoint.transform.position,firePoint.transform.rotation);
    }

    private void HealthTimer()
    {
        if (healthCanCountdown)
        {
            PlayerEntity.Health -= (Time.deltaTime * playerData.timeReduction);
        }
    }

    public void Heal()
    {
        playerData.healCharges -= 1;

        PlayerEntity.Health += playerData.healAmount;
        if(PlayerEntity.Health > PlayerEntity.maxHealth)
        {
            PlayerEntity.Health = PlayerEntity.maxHealth;
        }
    }

    public void BoostAttack()
    {
        combatData.damage *= 2;
    }

    public void ResetAttack()
    {
        combatData.damage /= 2;
    }

    public void ResetHealth()
    {
        PlayerEntity.Health = PlayerEntity.maxHealth;
    }

    public void ResetHealCharges()
    {
        playerData.healCharges = playerData.maxHealCharges;
    }

    public void ResetDeathState()
    {
        PlayerEntity.Health = PlayerEntity.maxHealth;
        invincible = false;
        isReal = true;
        //FreezePos = !FreezePos;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        
    }

    IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(playerData.iFrameTime);
        invincible = false;
    }

    IEnumerator SpriteFlicker()
    {
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer.enabled = !SpriteRenderer.enabled;
    }

    public LayerMask PortalLayer
    {
        get => portalLayer;
    }

    public void SetCurrentScene(SceneDetails currScene)
    {
        PrevScene = CurrentScene;
        CurrentScene = currScene;
    }
    //Reseting the position of the player
    public void setPosition(){
        if(spikeCheckPoint != null){
            playerTransform.position = spikeCheckPoint;
            RB.velocity = new Vector2(0,0);
        }
    }
    //The manual setting the checkpoint position
    public void setCheckPointPos(Vector2 newPos){
        spikeCheckPoint = newPos;
    }
    //Raycast setting checkpoint
    public void setCheckPointPosRay(){ 
        if(CheckFloorType().collider != null){
            var floor = CheckFloorType().collider.gameObject;
            if(CheckIfGrounded() && floor.tag == "Ground" && !invincible)
            {
                spikeCheckPoint = new Vector2(playerTransform.position.x,playerTransform.position.y);
            }
        }
    }
    //Used for respawning on death
    public void respawnPosition(){
        this.GetComponent<Transform>().position = spawnPoint;
        firstReload = true;
        ResetHealCharges();
    }
    //Used to reset the boundary of the camera on reload
    private void firstReloadCamConfine(){
        var colliderBound = GameObject.Find("WorldBoundary");
        newWorldBound = colliderBound.GetComponent<Collider2D>();
        setConfiner(newWorldBound);
        firstReload = false;
    }
    public void setSpawnPosition(Vector2 resetSpawnPoint)
    {
        this.spawnPoint = resetSpawnPoint;
    }

    #endregion
}
