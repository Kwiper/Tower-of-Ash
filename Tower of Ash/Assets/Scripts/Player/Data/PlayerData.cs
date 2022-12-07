using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData",menuName ="Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject 
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public bool unlockedDoubleJump = false;
    public float jumpVelocity = 21f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform;
    public float wallCheckDistance = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Jump State")]
    public bool unlockedWallJump = false;   
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1,2);

    [Header("Dash State")]
    public bool unlockedDash = false;
    public float dashCooldown = 0.5f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.5f;

    [Header("Attack State")]
    public float attackCooldown = 0.3f;

    [Header("Fireball State")]
    public bool unlockedFireball = false;
    public float fireballCooldown = 0.5f;
    public float fireballTime = 0.1f;

    [Header("Charge Attack")]
    public bool unlockedChargeAttack = false;
    public float chargeVelocity = 15f;

    [Header("Pogo")]
    public float pogoVelocity = 25f;

    [Header("Timer")]
    public float timeReduction = 0.833333337f;

    [Header("Invincibility")]
    public float iFrameTime = 0.5f;

    [Header("Heal State")]
    public bool unlockedHealing = false;
    public float healAmount = 25f;
    public int healCharges = 3;
    public int maxHealCharges = 3;

    [Header("Tinder")]
    public int tinder = 0;
    public List<Vector2> CollectedTinderCacheLocations;


    [Header("Doors")]
    public List<int> keys;
    public List<bool> keysCollected;

    [Header("Map")]
    public bool unlockedMap = false;
    public List<int> ids;
    public int positionId;

    [Header("Tutorials")]
    public bool movementTutorial = false;
    public bool combatTutorial = false;
    public bool lookTutorial = false;

}
