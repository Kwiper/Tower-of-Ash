using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireball : MonoBehaviour
{
    [SerializeField]
    private string tagName;

    private Entity target;

    private Player player;

    [SerializeField]
    private CombatData combatData;

    private Rigidbody2D rb;

    int direction;

    GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = FindObjectOfType<Boss>().gameObject;
        rb = GetComponent<Rigidbody2D>();

        direction = boss.GetComponent<Boss>().FacingDirection;
        rb.velocity = new Vector2(direction * combatData.projectileSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            target = collision.gameObject.GetComponentInParent<Entity>();
            target.SetDamage(combatData.projectileDamage);

            target.SetKnockback(direction);

            player = collision.gameObject.GetComponentInParent<Player>();
            player.StateMachine.ChangeState(player.HitState);

            collision.gameObject.GetComponentInParent<TimeStop>().StopTime(0.05f, 10, 0.2f);

            Destroy(gameObject);

        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
