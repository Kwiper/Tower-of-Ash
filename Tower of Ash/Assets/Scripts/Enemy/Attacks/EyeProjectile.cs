using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeProjectile : MonoBehaviour
{
    [SerializeField]
    private string tagName;

    [SerializeField]
    private CombatData combatData;

    private Entity target;

    private Player player;

    Rigidbody2D rb;

    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(combatData.projectileSpeed * direction, 0);

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

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
