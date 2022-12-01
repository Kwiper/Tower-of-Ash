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

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        if (CheckIfPlayerInRange())
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position,10 * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
