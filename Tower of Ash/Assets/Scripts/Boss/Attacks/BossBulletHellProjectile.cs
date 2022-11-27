using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletHellProjectile : MonoBehaviour
{
    [SerializeField]
    private string tagName;

    private Entity target;

    private Player player;

    [SerializeField]
    private CombatData combatData;

    private Rigidbody2D rb;

    public Vector2 direction;

    int knockbackDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = direction * 15;

        if (direction.x >= 0)
        {
            knockbackDir = 1;
        }
        else
        {
            knockbackDir = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            if (!collision.gameObject.GetComponentInParent<Player>().invincible)
            {

                target = collision.gameObject.GetComponentInParent<Entity>();
                target.SetDamage(combatData.projectileDamage);

                target.SetKnockback(knockbackDir);

                player = collision.gameObject.GetComponentInParent<Player>();
                player.StateMachine.ChangeState(player.HitState);
                player.isHit = true;

                collision.gameObject.GetComponentInParent<TimeStop>().StopTime(0.05f, 10, 0.2f);
            }
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
