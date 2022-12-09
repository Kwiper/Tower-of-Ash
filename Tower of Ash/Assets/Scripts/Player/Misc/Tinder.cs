using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tinder : MonoBehaviour
{

    [SerializeField]
    PlayerData playerData;

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    int tinderReward;

    [SerializeField]
    float range;

    Player player;

    Rigidbody2D rb;

    AudioSource audioSource;
    public AudioClip collection;

    float timer = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        audioSource = FindObjectOfType<AudioSource>();
        timer = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            rb.velocity = new Vector2(0,0);
        }


        if (CheckIfPlayerInRange())
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position,10 * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == ("Player"))
        {
            playerData.tinder += tinderReward;
            Destroy(gameObject);
        }
    }

    bool CheckIfPlayerInRange()
    {
        return Physics2D.OverlapCircle(transform.position, range, playerLayer);
    }


}
