using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmberProjectile : MonoBehaviour
{
    [SerializeField]
    private string tagName;

    [SerializeField]
    private CombatData combatData;

    private Entity target;

    private Player player;

    public Vector2 angle;

    Rigidbody2D rb;

    public int direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = combatData.projectileSpeed * angle;

        if(rb.velocity.x >= 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        var swingingToAdd = GameObject.FindObjectsOfType<Platform>();
        for (int i = 0; i < swingingToAdd.Length; i++) 
        {
            Physics2D.IgnoreCollision(swingingToAdd[i].gameObject.GetComponent<PolygonCollider2D>(), GetComponent<BoxCollider2D>(), true);

        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            target = collision.gameObject.GetComponent<Entity>();

            target.SetDamage(combatData.projectileDamage);

            target.SetKnockback(direction);

            player = collision.gameObject.GetComponentInParent<Player>();
            player.StateMachine.ChangeState(player.HitState);
            player.isHit = true;

            collision.gameObject.GetComponentInParent<TimeStop>().StopTime(0.05f, 10, 0.2f);

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }

    }
}
