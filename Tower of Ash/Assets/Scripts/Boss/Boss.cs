using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public BossIdleState IdleState { get; private set; }
    public BossAttackState AttackState { get; private set; }
    public BossWalkState WalkState { get; private set; }
    public BossPillarState PillarState { get; private set; }
    public BossLungeState LungeState { get; private set; }
    public BossJumpAttackState JumpAttackState { get; private set; }
    public BossFireballState FireballState { get; private set; }
    public BossBulletHellState BulletHellState { get; private set; }
    public BossBulletHellCharge BulletHellCharge { get; private set; }
    public BossFallState FallState { get; private set; }
    public BossRegalState RegalState { get; private set; }

    public AudioClip slam;
    public AudioClip fireball;
    public AudioClip dash;
    public AudioClip charge;
    public AudioClip swing;
    public AudioClip scrape;
    public AudioClip pillars;
    public AudioClip bulletHell;
    public AudioClip fall;

    [SerializeField]
    private Transform AggroPoint;
    public LayerMask playerLayer;

    [SerializeField]
    private Transform BoundaryPoint;
    public LayerMask groundLayer;
    public LayerMask boundaryLayer;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float AggroRadius;

    [SerializeField]
    private float ProjectileRadius;

    public Transform bulletHellPoint;
    public Transform fireballPoint;
    public GameObject Fireball;
    public GameObject Pillar;

    public GameObject Bullets;
    public GameObject[] FallPillars;

    public bool canBulletHell = true;

    public override void Awake()
    {
        base.Awake();
        IdleState = new BossIdleState(this, StateMachine, "idle");
        AttackState = new BossAttackState(this, StateMachine, "attack");
        WalkState = new BossWalkState(this, StateMachine, "walk");
        PillarState = new BossPillarState(this, StateMachine, "pillar");
        LungeState = new BossLungeState(this, StateMachine, "lunge");
        JumpAttackState = new BossJumpAttackState(this, StateMachine, "jumpAttack");
        FireballState = new BossFireballState(this, StateMachine, "fireball");
        BulletHellState = new BossBulletHellState(this, StateMachine, "bulletHell");
        FallState = new BossFallState(this, StateMachine, "fall");
        BulletHellCharge = new BossBulletHellCharge(this, StateMachine, "bulletHellCharge");
        RegalState = new BossRegalState(this, StateMachine, "regal");

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
        StateMachine.Initialize(RegalState);
    }

    public override void Update()
    {
        base.Update();
        StateMachine.CurrentState.LogicUpdate();

    }

    public bool CheckIfTouchingGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    public bool CheckIfPlayerInAggroRange()
    {
        return Physics2D.OverlapCircle(AggroPoint.position, AggroRadius, playerLayer);
    }

    public bool CheckIfPlayerInProjectileRadius()
    {
        return Physics2D.OverlapCircle(AggroPoint.position, ProjectileRadius, playerLayer);
    }

    public bool CheckIfBoundaryDetected()
    {
        return Physics2D.OverlapCircle(BoundaryPoint.position, 0.5f, boundaryLayer);
    }

    public void CastFireball()
    {
        GameObject instance = Instantiate(Fireball, fireballPoint.transform.position, fireballPoint.transform.rotation);
        instance.GetComponent<BossFireball>().direction = new Vector2(FacingDirection, 0);
        AudioSource.PlayOneShot(scrape);
    }

    public void FireBulletHell(float additionalAngle)
    {
        float angleStep = 45f;

        float angle = 0f + additionalAngle;

        for(int i = 0; i < 8; i++)
        {
            float projectileDirX = bulletHellPoint.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float projectileDirY = bulletHellPoint.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector2 projectileVector = new Vector2(projectileDirX, projectileDirY);
            Vector2 projectileMoveDirection = (projectileVector - new Vector2(bulletHellPoint.position.x,bulletHellPoint.position.y)).normalized;

            GameObject instance = Instantiate(Bullets, bulletHellPoint.position, bulletHellPoint.rotation);
            instance.GetComponent<BossBulletHellProjectile>().direction = projectileMoveDirection;

            angle += angleStep;
        }
        AudioSource.PlayOneShot(fireball);
    }

    public void CastPillar()
    {
        float distance = 3f;

        for(int i = 0;i < 10; i++)
        {
            GameObject p1 = Instantiate(Pillar, new Vector2(this.transform.position.x + distance, -3f),this.transform.rotation);
            GameObject p2 = Instantiate(Pillar, new Vector2(this.transform.position.x - distance, -3f), this.transform.rotation);

            distance += 3f;
        }
    }

    public void CastFallPillar()
    {
        float distance = 2.5f;
        for(int i = 0; i < 3; i++)
        {
            GameObject p1 = Instantiate(FallPillars[i], new Vector2(this.transform.position.x + distance, -3f), this.transform.rotation);
            GameObject p2 = Instantiate(FallPillars[i], new Vector2(this.transform.position.x - distance, -3f), this.transform.rotation);

            distance += 2.5f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AggroPoint.position, AggroRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(AggroPoint.position, ProjectileRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(BoundaryPoint.position, 0.5f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);

    }

}
