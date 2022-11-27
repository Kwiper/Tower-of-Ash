    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private CombatData combatData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Entity PlayerEntity { get; private set; }
    public SpriteRenderer SpriteRenderer;
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
    public bool isReal = false;
    public bool hitHead = false;
    public bool manualCheckPointSection = false;
    public bool hitSpike = false;
    public Vector2 spawnPoint = new Vector2(3,5);
    private Vector2 lookPosDefault;
    private Vector2 spikeCheckPoint;
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

    #endregion

    #region Scene Detail Variables
    public SceneDetails CurrentScene { get; private set;}
    public SceneDetails PrevScene { get; private set;}
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
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        PlayerEntity = GetComponent<Entity>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        FacingDirection = 1;
        var pos = new Vector2(gameObject.transform.Find("CameraPoint").position.x,gameObject.transform.Find("CameraPoint").position.y);
        lookPosDefault = pos;
        ResetHealCharges();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        SetLookDisplacement();
        //Debug.Log(CheckIfGrounded());
        if(InputHandler.chargeHeld){
            SpriteRenderer.color = Color.gray;
        }
        else{
            SpriteRenderer.color = Color.white;
        }

        StateMachine.CurrentState.LogicUpdate();

        HealthTimer();

        if (PlayerEntity.InKnockback)
        {
            SetVelocityX(PlayerEntity.Knockback);
        }

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


        // When HP reaches 0, load upgrade scene
        if (PlayerEntity.Health <= 0)
        {
            PlayerEntity.Health = PlayerEntity.maxHealth;
            invincible = false;
            isReal = true;
            RB.constraints = RigidbodyConstraints2D.FreezePosition;
            //FreezePos = !FreezePos;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
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
        //Debug.Log(InputHandler.NormLookInputX);
        //Debug.Log(InputHandler.NormLookInputY);        
        cameraPoint.position = new Vector2(gameObject.GetComponent<Transform>().position.x+InputHandler.NormLookInputX, gameObject.GetComponent<Transform>().position.y+InputHandler.NormLookInputY+1);
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

    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
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

    public void ResetHealCharges()
    {
        playerData.healCharges = playerData.maxHealCharges;
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

    public void setPosition(){
        if(spikeCheckPoint != null){
            playerTransform.position = spikeCheckPoint;
            RB.velocity = new Vector2(0,0);
        }
    }
    public void setCheckPointPos(Vector2 newPos){
        spikeCheckPoint = newPos;
    }

    public void setCheckPointPosRay(){
        //yield return new WaitForSeconds(0.2f);
        //Debug.Log("The player's spike check point is " + spikeCheckPoint);   
        if(CheckFloorType().collider != null){
            var floor = CheckFloorType().collider.gameObject;
            if(CheckIfGrounded() && floor.tag == "Ground" && !invincible)
            {
                spikeCheckPoint = new Vector2(playerTransform.position.x,playerTransform.position.y);
            }
        }
    }

    public void respawnPosition(){
        playerTransform.position = spawnPoint;
        ResetHealCharges();
    }

    #endregion
}
