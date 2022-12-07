using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshProjectile : MonoBehaviour
{
    [SerializeField]
    private string tagName;

    [SerializeField]
    private CombatData combatData;

    private Entity target;

    private Player player;

    Rigidbody2D rb;

    public int direction;

    AudioSource audioSource;
    [SerializeField]
    AudioClip splat;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        rb.velocity = new Vector2(combatData.projectileSpeed * direction,0);
        
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

            player = collision.gameObject.GetComponentInParent<Player>();
            player.StateMachine.ChangeState(player.HitState);
            player.isHit = true;

            collision.gameObject.GetComponentInParent<TimeStop>().StopTime(0.05f, 10, 0.2f);

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 10)
        {
            audioSource.PlayOneShot(splat);
            Destroy(gameObject);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
