using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private string tagName;

    private Entity target;

    [SerializeField]
    private CombatData combatData;

    private Rigidbody2D rb;

    int direction;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        
        direction = player.GetComponent<Player>().FacingDirection;
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
            target = collision.gameObject.GetComponent<Entity>();
            target.SetDamage(combatData.projectileDamage);

            target.SetKnockback(direction);


            Destroy(gameObject);

        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
