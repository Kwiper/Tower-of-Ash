using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    Player player;
    [SerializeField]
    private string tagName;

    [SerializeField]
    private CombatData combatData;

    private Entity target;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Transform wallCheck;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (!GroundCheck() || WallCheck())
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    bool WallCheck()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.5f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            target = collision.gameObject.GetComponent<Entity>();

            target.SetDamage(combatData.projectileDamage);

            target.SetKnockback(-player.FacingDirection);

            player = collision.gameObject.GetComponentInParent<Player>();
            player.StateMachine.ChangeState(player.HitState);
            player.isHit = true;

            collision.gameObject.GetComponentInParent<TimeStop>().StopTime(0.05f, 10, 0.2f);
        }

    }

    public void AnimationTrigger()
    {
        Destroy(gameObject);
    }

}
