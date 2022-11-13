using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobesProjectile : MonoBehaviour
{
    [SerializeField]
    private string tagName;

    [SerializeField]
    private CombatData combatData;

    private Entity target;

    private Player player;

    public Vector2 projectileDirection;

    int direction;

    float initTimer = 0.5f;

    float stopTrackTimer = 0.5f;

    bool canTrack;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canTrack = false;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        initTimer -= Time.deltaTime;
        if(initTimer <= 0 && stopTrackTimer > 0)
        {
            canTrack = true;
        }

        if (canTrack)
        {
            stopTrackTimer -= Time.deltaTime;

            projectileDirection = (Vector2)player.transform.position - rb.position;
            projectileDirection.Normalize();

            if(stopTrackTimer <= 0)
            {
                canTrack = false;
            }
        }

        if (projectileDirection.x > 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        float rotateAmount = Vector3.Cross(projectileDirection, transform.right).z;
        rb.angularVelocity = -0.01f * rotateAmount;
        rb.velocity = projectileDirection * combatData.projectileSpeed;

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
